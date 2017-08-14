﻿using ReactNative.Bridge;
using ReactNative.DevSupport;
using ReactNative.Modules.Core;
using ReactNative.Modules.DeviceInfo;
using ReactNative.Modules.DevSupport;
using ReactNative.Tracing;
using ReactNative.UIManager;
using ReactNative.UIManager.Events;
using System;
using System.Collections.Generic;
#if !WINDOWS_UWP
using System.Windows;
#endif

namespace ReactNative
{
    /// <summary>
    /// Package defining core framework modules (e.g., <see cref="UIManagerModule"/>). 
    /// It should be used for modules that require special integration with
    /// other framework parts (e.g., with the list of packages to load view
    /// managers from).
    /// </summary>
    class CoreModulesPackage : IReactPackage
    {
        private readonly IReactInstanceManager _reactInstanceManager;
        private readonly Action _hardwareBackButtonHandler;
        private readonly UIImplementationProvider _uiImplementationProvider;

        public CoreModulesPackage(
            IReactInstanceManager reactInstanceManager,
            Action hardwareBackButtonHandler,
            UIImplementationProvider uiImplementationProvider)
        {
            _reactInstanceManager = reactInstanceManager;
            _hardwareBackButtonHandler = hardwareBackButtonHandler;
            _uiImplementationProvider = uiImplementationProvider;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Caller manages scope of returned list of disposables.")]
        public IReadOnlyList<INativeModule> CreateNativeModules(ReactContext reactContext)
        {
            var uiManagerModule = default(INativeModule);
            using (Tracer.Trace(Tracer.TRACE_TAG_REACT_BRIDGE, "createUIManagerModule").Start())
            {
                var viewManagerList = _reactInstanceManager.CreateAllViewManagers(reactContext);
                uiManagerModule = new UIManagerModule(
                    reactContext, 
                    viewManagerList,
                    _uiImplementationProvider);
            }

            return new List<INativeModule>
            {
                //new AnimationsDebugModule(
                //    reactContext,
                //    _reactInstanceManager.DevSupportManager.DevSettings),
                //new SystemInfoModule(),
                new DeviceEventManagerModule(reactContext, _hardwareBackButtonHandler),
                new DeviceInfoModule(reactContext),
                new ExceptionsManagerModule(_reactInstanceManager.DevSupportManager),
                new Timing(reactContext),
                new SourceCodeModule(
                    _reactInstanceManager.SourceUrl,
                    _reactInstanceManager.DevSupportManager.SourceMapUrl),
                uiManagerModule,
                //new DebugComponentOwnershipModule(reactContext),
            };
        }

        public IReadOnlyList<IViewManager> CreateViewManagers(
            ReactContext reactContext)
        {
            return new List<IViewManager>(0);
        }
    }
}
