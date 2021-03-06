For overall release strategy for `react-native-windows`, see [Releases](../../Releases.md). 

This document describes the release strategy for the _vnext_ effort around [React Native for Windows C++](../vnext/README.md). 

The _vnext_ versioning will generally follow the same strategy as outlined in current [Releases](../../Releases.md). Specifically,

- We release in lock-step with [facebook/react-native](https://github.com/facebook/react-native). I.e., for every release of `react-native`, we create a release of `react-native-windows` with a matching major version.
- We will use a pre-release flag: `-vnext` - to differentiate this C++ implementation followed by the current vnext version number.

For example: 

```
...
react-native@0.57.* -> react-native-windows@0.57.0-vnext.*
react-native@0.58.* -> react-native-windows@0.58.0-vnext.*
...
```
