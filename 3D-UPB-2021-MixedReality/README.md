# 3D-UPB-2021-XR | Mixed Reality Workshop

# Quick onboarding

## So what's up with Mixed Reality? 👓

As  Virtual Reality (VR) allows you to explore a fully virtual world using headsets and Augmented Reality (AR) overlays virtual elements on top of the physical world, **Mixed Reality (MR)** merges the virtual and the physical, allowing the user to interact with both the physical and virtual environments in real-time. In **MR**, the 3D elements you are presented with will react as they would in the real world - at least this is the big picture and idealization of **MR**.

>⭐ Note that you may encounter **MR** standing for either **Mixed** or **Merged Reality**. Use whichever suits you.

![VR - MR- AR](https://www.researchgate.net/profile/Kaushik-Parida/publication/349459034/figure/fig1/AS:998584967241732@1615092617710/Schematic-illustration-of-virtual-reality-augmented-reality-and-mixed-reality.ppm)

![enter image description here](https://miro.medium.com/max/5634/1*A83dEcq8kLPyPQ60aohFvQ.png)

### But there's a catch 🤨

Since **MR** is usually achieved using headsets, what we'll be working on isn't *really* mixed reality - it wouldn't be an option to ask you to buy expensive headsets, and we do not have enough at the moment to lend to you. As such, I've come up with a second best option, which uses **AR** to overlay the virtual world and uses **MR** techniques to interact with them. Think of it as *Soft MR* or *AR++*.

### What we'll be using / what we'll need
- Unity (2020.3+)
- Visual Studio 2017/2019
- ManoMotion SDK
- ARFoundation
- AR capable Android/iOS* device, with Android 7+
- Active Internet connection on smartphone during testing (ManoMotion's license verification)
>*Did not test for iOS, but both ManoMotion and Unity are capable of building to iOS - we'll see

#### ARFoundation
Multi-platform API used to develop AR apps. Can be used to simultaneously develop apps for ARCore (Android) or ARKit (iOS)

#### ManoMotion SDK
ManoMotion is a cool framework which allows building Android/iOS apps in AR, using your hand as input. It employs some fancy computer vision algorithms to recognize your hand and also has an SDK which allows you to code apps using gestures as input.

# Sneak peek on what we'll be implementing
Here's a GIF

![img desc](https://github.com/Andrei-Lapusteanu/3D-UPB-2021-XR-Workshop/blob/master/3D-UPB-2021-MixedReality/GIFs/ar-mr-app-small.gif)

**Also, check out the full demo here (https://www.youtube.com/watch?v=njgYgnUo0s0)**, relevant for you to get an idea of what functionalities to implement during this workshop session.

### But there's another catch (sorry) 😅

Developing apps with **ARFoundation** isn't the easiest thing to do (compared to non-XR dev). One big hindrance is that ARFoundation has yet to offer any sort of easy (or free) way to do *live/instant preview* - this means that you cannot run your app inside the Unity editor or debug them - which kind of leaves you guessing why something suddenly stopped working. If you're serious about developing apps, there are some not-so-cheap third party solutions (check out *AR Foundation Editor Remote* on the Unity Asset Store).

## OK, OK, but tell me what I'll be doing
Because there's a lot of setup to do for an XR app, I've already done all the AR init stuff, and some minimal interaction logic. I'll guide you through the existing project and then you'll have to program in some game logic in order to finish the basketball game. In short, you'll:
- Build a test scene in order to verify that everything works
- Build the basketball scene and test hand-physics object interaction
- Implement the throw mechanic, compute the throw force and animate the powerbar, add colldier(s) to the basketball hoops and write some trigger logic for score computation. Also, you'll be tasked to implement basketball spawning using a new gesture - basically everything presented in the demo video :)
- Feel free to implement/reimplement anything you want

# Let's get to work

## 1. Basic setup & ManoMotion testing

### 1.1. Smartphone setup 

**1.** You'll need to enable **Developer Mode**, **USB Debugging** and **Install via USB** on your device

**2.** Connect device to your PC/Laptop in **file transfer mode**, charging only mode won't be enough

### 1.2. Project setup & build test

**1.** Download/Clone this repo

**2.** **Make sure your Unity version is at least 2020.3**. I've tested on older versions (2019) and it didn't work

**3.**  Make sure your Unity version has an **Android or iOS module**. If not, make sure to install whichever you need from the Unity Hub > Installs panel

![enter image description here](https://i.imgur.com/pDZIbWk.png)

**4.** Open the Unity project

**5.** To make things a bit easier down the line, it's best to set a custom resolution for the **Game window** inside the Unity Editor. I've set mine to **2400 (width) x 1080 (height) landscape mode - I strongly advise you to do the same** as I've developed the demo only with landscape mode in mind

![enter image description here](https://i.imgur.com/tGoUtYk.png)

**6.** Go to **File > Build Settings...**

![enter image description here](https://i.imgur.com/9OiTLhc.png)

- Select Android/iOS platform and click **Switch Platform**
- Check if your device shows up in the **Run Device** combobox. If true, you can leave it or the **Default device** option selected
- At the top you'll see the **Scenes In Build**. I have provided you with a number of scenes in order to test different functionalities, more on this later. For now, **select only the scene numbered 2**, as pictured above

**7.** **Let's try doing a build.** Simply click on **Build And Run**, save the *.apk* wherever you want, name it however you fancy, and give Unity some time to think. First build of any project usually takes some time, expect +2 mins at least (it will depend strongly on your PC, if it takes you <1min, I really envy your setup 🙃)

**8.** Watch your phone as the progress bar nears completion. You'll probably have to **permit the app to install on your smartphone**

![enter image description here](https://i.imgur.com/nyDdbqY.jpg)

**9.** If all goes well, the app should be installed on your smartphone and it should automatically start

**10.** **Allow for permissions**, without a camera there's really nothing to work on

**11.** The app should have some debug info on screen. Simply put one hand in front of the camera and see if it recognizes it (shown by the presence of a white bounding box around the hand). **Try out the gestures shown in the GIF below**

> ⭐Hint: ManoMotion can be picky about hand recognition. It works best if there's sufficient light and a plain background.

![enter image description here](https://github.com/Andrei-Lapusteanu/3D-UPB-2021-XR-Workshop/blob/master/3D-UPB-2021-MixedReality/GIFs/xr-test-scene-demo.gif) 

## 2. Project structure

The provided project has been structured as follows:

![enter image description here](https://i.imgur.com/wWG8fFg.png)

#### 1-ManoMotion-SDK-ARFoundation
 This contains the ManoMotion SDK. It was installed using the *.unitypackage* file downloaded from ManoMotion's website
> ⭐Note: You're not required to make a ManoMotion account. I had to because I needed to download the file and also do some license setup - basically, each app you develop using ManoMotion's SDK requires a **Licence Key**, **Bundle ID** and an internet connection on your smartphone in order to run the app. Failing to do so will not let ManoMotion's scripts run - I've already setup this stuff in this project so no need to worry about it (but keep this in mind if you'll ever wish to develop your own app, for more info make an account and check out their docs)

#### 2-ManoMotion-Testing

The debug app you've just built on your phone. You can find the scene and some scripts here (more on this in a bit)

#### 3-ManoMotion-Game

The basketball game setup, split up into:
- MyResources: Materials (for textures), PhysicsMaterials (for the physics object), Prefabs (which will be instantiated in the scene), Textures
- Scenes (the game scene)
- Scripts (divided into folders by their purpose). More on all of this later.

## 3. ManoMotion's gestures & other relevant data

ManoMotion offers plenty of information we can work with. These are grouped in a number of ```enum``` data types. Here's a list relevant info we will need / you might want to use (if you wish to do further developments)

#### ManoClass
General classes for gesture recognition
```C#
ManoClass.NO_HAND
ManoClass.GRAB_GESTURE
ManoClass.POINTER_GESTURE
ManoClass.PINCH_GESTURE
```
#### ManoGestureTrigger
Active only in the frame in which the gesture is detected (hence the name 'trigger')
```C#
ManoGestureTrigger.NO_GESTURE
ManoGestureTrigger.CLICK
ManoGestureTrigger.RELEASE_GESTURE
ManoGestureTrigger.GRAB_GESTURE
ManoGestureTrigger.PICK
ManoGestureTrigger.DROP
```
#### ManoGestureContinuous
Active in all frames in which the gesture is detected (hence the name 'continuous')
```C#
ManoGestureContinuous.NO_GESTURE
ManoGestureContinuous.HOLD_GESTURE
ManoGestureContinuous.OPEN_HAND_GESTURE
ManoGestureContinuous.OPEN_PINCH_GESTURE
ManoGestureContinuous.CLOSED_HAND_GESTURE
ManoGestureContinuous.POINTER_GESTURE
```
#### HandSide
```C#
 HandSide.None
 HandSide.Backside
 HandSide.Palmside
```
### Here's how the debug info was grabbed
```C#
// All info for a hand
HandInfo handInfo = ManomotionManager.Instance.Hand_infos[0].hand_info;

// All gesture information regarding the hand
GestureInfo gestureInfo = handInfo.gesture_info;

/* Using the previously attained hand & gesture infos,
let's get relevant data (which will be displayed on-screen) */
ManoGestureTrigger gestTrigger = gestureInfo.mano_gesture_trigger;          // Trigger gesture
ManoClass gestClass = gestureInfo.mano_class;                               // Gesture class
HandSide gestHandSide = gestureInfo.hand_side;                              // Hand side
ManoGestureContinuous gestContinuous = gestureInfo.mano_gesture_continuous; // Continuous gesture
int gestState = gestureInfo.state;                                          // Gesture state
Vector3 palmPos = handInfo.tracking_info.palm_center;                       // Screen coords [0...1]

// Multiply screen coords with width and height of your display
palmPos.x *= Constants.SCREEN_WIDTH;
palmPos.y *= Constants.SCREEN_HEIGHT;
   ```
   
   > ⭐ Note:  All of this info can be also be viewed in the scripts @ 2-ManoMotion-Testing/Scripts

### Here's how you can use gestures as input

It's really no big deal, think of these gestures as a substitute for mouse/keyboard/controller input. You can use it during an **Update()** or **FixedUpdate()** call in any script.

```C#
// Update is called once per frame
void Update()
{
    // Desktop version 
    bool LMB_Down = Input.GetMouseButtonDown(0); // true if Left Mouse Button Down is registered in this frame

    // Grab hand info from ManoMotion's SDK
    HandInfo handInfo = ManomotionManager.Instance.Hand_infos[0].hand_info;

    // If LMB down (desktop) or grab trigger gesture (AR) detected this frame
    if(LMB_Down || handInfo.gesture_info.mano_gesture_trigger == ManoGestureTrigger.GRAB_GESTURE)
    {
        // Awesome code here ヾ(⌐■_■)ノ♪
    }
}
```

## 4. Basketball game intro

### Switch scenes

Let's switch to the basketball game scene (3-ManoMotion-Game > Scenes > 3-ManoMotion-Template)

### Object hierarchy

Let's briefly talk about the objects which compose this scene

![enter image description here](https://i.imgur.com/ZEw4f0G.png)

- **AR-Root**: Used to group everything AR and game related
- **AR Session**: Controls the life cycle of the AR app. In an AR scene, the session object should only be instantiated once (as shown above), as it is a global construct
- **AR Session Origin**: Transforms *trackables* (planar surfaces, feature points, scene elements, etc.) into their final position/rotation/scale
- **ARSessionMiddleReference**: Empty object only used to perform some scene scale adjustments
- **AR Camera**: The virtual camera. It will be your phone's actual camera and it will move/rotate according to the phone's motion. Must be the child of the above-described elements to permit proper positioning/rotation/scaling
- **ManoMotionHandVisualizer**: Overlays the 2D bounding box and its data on-screen
- **AR Default Plane**: The virtual plane instance which will be overlaid over physical surfaces
- **ManomotionManager**
- **ARGameInitController**: Contains the scripts which help the user setup the scene in 3 phases (as seen in demo video)
- **ARGameInitCanvas**: The UI canvas which helps the user setup the scene in 3 phases (as seen in demo video)
- **ARGameController**: Camera controller and object to add future scripts to (if needed)
- **ARGameCanvas**: In-game UI

### Scripts

Organized by their purpose. You'll mostly have to code in the **ToDoLogic** folder. Feel free to explore the other scripts if you want to get a better grip on how everything works.

### Build the scene & get accustomed to the input method

Before geting to work, let's build this scene (you should already know how to switch scenes by now and how to perform a build). If you haven't already, I advise you to watch the demo video in order to understand the way in which to perform the setup phases.

After getting the game to run and instantiating the virtual basketball court, you should be able to press the button in the top-right corner to spawn a basket ball. 
- Hover your open hand over it, when it highlights it means it can be interacted with
- Perform a *grab trigger gesture* - you should be able to virtually 'hold' the basketball. It will remain as such until you perform a *release trigger gesture*. Try to move the basketball while holding onto it.

## 5. Basketball game hands-on work (pun intended 😬) 

Most of the coding will be done in the scripts from the "ToDoLogic" folder.

### ❗ 0. In order to work easier

 As I've already said, instant preview isn't an option for now, and thus you won't have to implement too much stuff. In order to work easier, I've setup the project to take input from either mouse (desktop) or your hand (AR). As such, it's easier to test some parts of the app before building to your smartphone. If you want to do so, follow these steps in order to prototype for desktop:

- In the Unity Editor (inspector), on the **ARGameInitController** object, on the **ARInitManager** script, set **Global Game Mode** to **Desktop**

> ⭐Note: Don't forget to change it back when building to phone! 

- Drag&drop the *MyResources/Prefabs/ScenePrefab.prefab* to the **AR-Root** object. You should partially see the scene in the camera view. Move it until it's somewhat in the camera's field of view
- Hit play and debug as much as you want! But as you can imagine, you cannot test AR-related / gesture-related code this way

### 1.  Change release gesture to throw gesture

As we now want to throw the basketball instead of just releasing it, inside **HandColliderInputController.cs** script, inside the **Update()** function, inside the if statement regarding the ```ManoGestureTrigger.RELEASE_GESTURE```, change the action from *Release* to *Throw*

> ⭐ Note: As I guess you already know, press F12 on function calls to navigate to their implementation.

### 2. Compute throw power & update UI

- The ```HandleThrow()``` function calls ```ComputeThrowAction()``` inside the **HandlePhysicsObjectThrow.cs** script. In order to perform this action, we need to first compute the throw direction, power, update the UI, etc.
- Follow the ```//TODOs``` in the **UpdatePhysicsObjectThrowPower.cs** script to complete this task, I've left some comments to guide you
- The powerbar (slider) should continuously animate between its max and min values as long as the physics object is held in the air (following the grab gesture)

![img desc](https://github.com/Andrei-Lapusteanu/3D-UPB-2021-XR-Workshop/blob/master/3D-UPB-2021-MixedReality/GIFs/powerbar-gif.gif)

### 3. Compute throw direction and apply throw action

- Follow the ```//TODOs``` in the **HandlePhysicsObjectThrow.cs** script to complete this task, I've left some comments to guide you

### 4. Intermediary build

- Before going further, make another build to test if everything up until now works. Make such builds as frequently as you need them

### 5.  Basketball hoop collider & score 

- Follow the ```//TODOs``` in the **HoopColliderLogic.cs** script to complete this task, I've left some comments to guide you
- You'll have to open the prefab *MyResources/Prefabs/Basketball Hoop/BasketballHoopPrefab.prefab* and add a collider to the basketball hoop. **HoopColliderLogic.cs** will attach to this collider.

### 6. Gesture instantiation

- You'll have to instantiate a basketball when you perform a ```ManoGestureTrigger.PICK``` on the object named *PlayPlane* (you won't see it in the scene, it's a child of the virtual basketball court which is instantiated at the start of the game
- As always, follow the  ```//TODOs``` in the **HandColliderInputController.cs** script, inside the **Update()** function

### 7. Further developments (optional)

- Feel free to implement whatever else you like, I'm curious to see what you come up with

**Happy coding!**

![img desc](https://github.com/Andrei-Lapusteanu/3D-UPB-2021-XR-Workshop/blob/master/3D-UPB-2021-MixedReality/GIFs/happy-coding-gif.gif)
