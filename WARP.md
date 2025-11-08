# WARP.md

This file provides guidance to WARP (warp.dev) when working with code in this repository.

## Project Overview

**Erase Project IHC** is a Unity VR application targeting Meta Quest devices (Quest 2/Pro/3/3S). This is a human-computer interaction (IHC) project built with Unity 2022.3.62f2 LTS.

**Key Technologies:**
- Unity 2022.3.62f2 LTS
- Meta XR SDK v78.0.0 (Oculus integration)
- Android target (Quest platform)
- Android Min SDK: 32, Target SDK: 32
- Linear color space for VR optimization

## Build and Development Commands

### Opening the Project
Open the project in Unity Hub or directly launch Unity 2022.3 LTS:
```powershell
# Via Unity Hub (recommended)
# Navigate to Add -> Add project from disk -> Select C:\Users\jeffr\GitHub\Erase_Project_IHC
```

### Building for Quest
Since this is a Unity project, builds are done through the Unity Editor:
1. Open Unity Editor (File > Build Settings)
2. Ensure platform is set to **Android**
3. Target device: **Quest 2** (TargetQuest2 enabled in OculusSettings)
4. Build type: Development Build (for testing) or Release

### Testing in Editor
Unity Play Mode testing with XR simulation:
- Press Play button in Unity Editor
- XR Device Simulator can be used for development without hardware

### Deploying to Quest Device
1. Enable Developer Mode on Quest headset
2. Connect via USB or use wireless ADB
3. In Unity: File > Build And Run
4. Or build APK and sideload via ADB:
```powershell
# Install built APK to Quest
adb install -r path\to\eraseproject.apk

# Check logcat for debugging
adb logcat -s Unity
```

## Project Structure

### Assets Organization
```
Assets/
├── MetaXR/          # Meta XR SDK integration (imported package)
├── Oculus/          # Oculus-specific assets (imported package)
├── Plugins/         # Native plugins
│   └── Android/     # Android manifest and libraries
├── Resources/       # Runtime-loaded assets
│   ├── OculusRuntimeSettings.asset
│   ├── InputActions.asset
│   └── ImmersiveDebuggerSettings.asset
├── Scenes/          # Unity scenes
│   └── SampleScene.unity (main/default scene)
└── XR/              # XR configuration
    ├── Loaders/     # XR loader settings
    └── Settings/    # Oculus and XR settings
```

### Key Configuration Files
- `Packages/manifest.json` - Unity package dependencies
- `ProjectSettings/ProjectSettings.asset` - Main project configuration
- `Assets/XR/Settings/OculusSettings.asset` - Oculus XR configuration
- `Assets/Resources/OculusRuntimeSettings.asset` - Runtime VR settings
- `Assets/Plugins/Android/AndroidManifest.xml` - Android/Quest manifest

### Important Settings
**Oculus Settings** (`Assets/XR/Settings/OculusSettings.asset`):
- Stereo rendering: Multi-view on Android (optimized for Quest)
- Target device: Quest 2 enabled
- Features: Face tracking (visual + audio), body tracking enabled
- Depth submission and shared depth buffer for better performance

**Build Configuration:**
- Company: utec
- Product: eraseproject
- Bundle identifier: com.utec.eraseproject
- Default resolution: 1920x1080
- Color space: Linear (required for physically-based rendering in VR)

## Architecture Notes

### VR Framework
This project uses the **Meta XR SDK** (v78.0.0) which provides:
- XR interaction system
- Hand tracking capabilities
- Face tracking (visual and audio-based)
- Body tracking
- Passthrough API support
- Meta avatars integration

The XR Management system handles loading the Oculus XR Plugin at runtime.

### Scene Setup
Currently contains a single scene (`SampleScene.unity`). This is configured as both the default scene and the only scene in the build.

### Rendering Configuration
- **Forward rendering** (default mobile path)
- **Linear color space** for accurate lighting in VR
- **Multi-view stereo rendering** on Android for performance
- No custom render pipeline (using built-in)

### Input System
Project includes `InputActions.asset` in Resources, suggesting use of Unity's new Input System for VR controller mapping.

## Platform-Specific Notes

### Android/Quest Development
- **VR headtracking required** - specified in Android manifest
- **Supported devices**: All Quest models (quest, quest2, questpro, quest3, quest3s)
- **VR category intent** - launches as VR app from Quest home
- **Focus aware** - handles headset removal properly
- **Telemetry GUID**: 6cf43aa1-161c-4439-9cf7-c475a672bded

### Performance Considerations
- Multi-view rendering reduces draw calls
- Shared depth buffer enabled for better occlusion
- Phase sync enabled for reduced latency
- Symmetric projection for optimal performance
- Subsampled layout for efficiency

## Adding New Scripts
Create C# scripts in an organized structure:
```powershell
# Suggested organization (currently no custom scripts exist)
Assets/
├── Scripts/
│   ├── Core/           # Core game systems
│   ├── Interaction/    # VR interaction handlers
│   ├── UI/            # User interface
│   └── Utilities/     # Helper scripts
```

All scripts should:
- Inherit from MonoBehaviour or appropriate Unity base class
- Follow C# naming conventions (PascalCase for public, camelCase for private)
- Consider VR-specific performance (minimize GC allocations)
- Test in both Editor XR simulator and on-device

## Git Workflow
The `.gitignore` is configured to exclude:
- Unity-generated folders (Library, Temp, Obj, Build, Logs)
- IDE files (.vs, .vscode, .idea)
- Auto-generated solution files (*.csproj, *.sln)
- Build artifacts (*.apk, *.aab)
- User-specific settings (UserSettings)

**Always commit**:
- `.meta` files alongside their assets (critical for Unity)
- Scene files and prefabs
- Custom scripts and assets
- Configuration in ProjectSettings (tracked in repo)

## Debugging VR Applications

### Unity Console
Monitor the Unity Console (Window > General > Console) for errors and warnings.

### On-Device Debugging
```powershell
# Connect to Quest and view Unity logs
adb logcat -s Unity

# Monitor all logs
adb logcat | Select-String "Unity|AndroidRuntime"

# Check device connection
adb devices
```

### Meta Quest Developer Hub
Use Meta Quest Developer Hub for advanced debugging, performance profiling, and wireless deployment.

## Common Issues

### Missing Meta XR SDK
If Meta XR components are missing, reimport from Package Manager or download from Meta's developer portal.

### Build Failures
- Ensure Android SDK/NDK paths are configured in Unity Preferences
- Check that Target API Level matches project settings (API 32)
- Verify keystore settings for release builds

### XR Not Working in Editor
Enable XR Plugin Management in Project Settings and ensure Oculus is checked for PC/Android platforms.

### Performance Issues on Quest
- Profile using Unity Profiler with Quest connected
- Check texture compression (ASTC for Android)
- Reduce draw calls via batching
- Optimize shadow settings and LOD
