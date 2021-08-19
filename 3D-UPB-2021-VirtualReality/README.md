
# 3D-UPB-2021-XR | Mixed Reality Workshop

# Quick onboarding

## So what's up with Virtual Reality?

**Virtual Reality (VR)** allows us to interact with a fully virtual world and aims to heighten our immersion with the digital environment. The technology requires specialized headsets, which present **stereoscopic 3D** images to the user (two images which are offset for each eye by a given distance, which combine in the brain and give us 3D perception).

In the last few years, **VR** as a technology has become more advanced and affordable, you've probably already experienced it in one form or another - the immersiveness which it offers surpasses other forms of media - whether it's here to stay or just another *fad* it's still up for debate.

One of the cheapest solutions to experience **VR** is using a smartphone holder, through which you can view the screen, which is split in 2, through a couple of lenses. It's not the best solution by far, due to the not-so-awesome build quality and lack of proper adjustments, buuut it's gets the job done, and as an intro into **VR** development, it's pretty good.

### What we'll be using / what we'll need

- Unity (2020.3+)
- Visual Studio 2017/2019
- Google Cardboard XR Plugin for Unity
- Mid-range smartphone (Android preferably, did not test for iOS) 

# Sneak peek on what we'll be implementing

Basically, I've made a simple and silly FPS game which currently works on PC (keyboard + mouse), and I'll show you how we can convert this into a VR game. There will be some simple ```//TODOs``` in order to program in some additional gameplay logic (more on this later).
> Plot: You're finally on vacation, but those kids and their drones keep buzzing around and you've had enough. Good thing you've brought your water balloon cannon and can shoot them down.
> 
> Disclaimer: There's no hate towards drones, they're cool, consider the cannon-wielding protagonist as the villain in this game 😁

[VR Demo (click here)](https://www.youtube.com/watch?v=mvYk8ZkUaXU)

# Let's get to work

### ❗ Please make sure to disable your antivirus (especially if you're using anything else that Windows Defender). Otherwise, you could run into errors while building the app to your phone, which will leave you stuck.

## 1. Smartphone setup

**1.** You'll need to enable **Developer Mode**, **USB Debugging** and **Install via USB** on your device

**2.** Connect device to your PC/Laptop in **file transfer mode**, charging only mode won't be enough

## 2. Project initial setup

**1.** Download/Clone this repo

**2.** **Make sure your Unity version is at least 2020.3**. To my knowledge, older versions could work, but downgrading the project's version is never a smart choice in Unity.

**3.**  Make sure your Unity version has an **Android or iOS module**. If not, make sure to install whichever you need from the Unity Hub > Installs panel

![enter image description here](https://i.imgur.com/pDZIbWk.png)

**4.** Open the Unity project

**5.** To make things a bit easier down the line, it's best to set a custom resolution for the **Game window** inside the Unity Editor. I've set mine to **2400 (width) x 1080 (height) landscape mode - I strongly advise you to do the same** as I've developed the demo only with landscape mode in mind.

![enter image description here](https://i.imgur.com/tGoUtYk.png)

**6.** Go to **File > Build Settings...**, select Android/iOS platform and click **Switch Platform**. What for Unity to reimport its assets.

## 4. Play test
Before diving into VR, let's quickly test the game. Simply open the scene **Drone-Shooter-Scene** and hit Play. Standard PC controls apply.

## 5. VR support setup

**Google VR SDK for Unity** was the go-to SDK for Android VR development in Unity up until ~2020. It hasn't seen any updates since 2019, and since **Unity 2020.1**, the package was deprecated and removed - hence, we won't be able to use it (although, personally, I think it had some nice features which the current XR plugin lacks).

As such, we'll be using the currently supported method for VR development, which is through the **Google Cardboard XR Plugin for Unity**.
> ⭐ Note: This requires Unity 2019.4.25f1 or later

The **quickstart guide** for the plugin can be found here https://developers.google.com/cardboard/develop/unity/quickstart#build_your_project, but I'll also write down here what needs to be configured.

 - **Git** should be installed on your computer

 - In Unity, go to  **Window**  >  **Package Manager**.

 - Click  **+**  and select  **Add package from git URL**.

 - Paste ```https://github.com/googlevr/cardboard-xr-plugin.git``` into the text entry field
	-	On the right panel, you can import the **Hello Cardboard** sample, if you wish
	
 - Go to **File > Build Settings > Player Settings**
 
 - Under **Resolution and Presentation** 
	 
	- Disable **Optimized Frame Pacing** 
		
	-  Set the **Default Orientation*** to **Landscape Left or Right**
		
- Under **Other Settings**

	- At **Graphics APIs** leave only **OpenGLES 3**
		 
	- Select ```IL2CPP``` in **Scripting Backend**
		 
	- Select desired architectures by choosing `ARMv7`, `ARM64`, or both in **Target  Architectures**
		 
	- Select  `Require`  in  **Internet Access**
	
- Under **Publishing Settings**
	 
	- In the **Build**  section, select  `Custom Main Gradle Template`  and  `Custom Gradle Properties Template`

```JS
implementation 'androidx.appcompat:appcompat:1.0.0'  
implementation 'androidx.constraintlayout:constraintlayout:1.1.3'  
implementation 'com.google.android.gms:play-services-vision:15.0.2'  
implementation 'com.google.android.material:material:1.0.0'  
implementation 'com.google.protobuf:protobuf-javalite:3.8.0'
```

- Add the following lines to `Assets/Plugins/Android/gradleTemplate.properties` (open with Notepad/Notepad++)
		 
```JS
android.enableJetifier=true        
android.useAndroidX=true
```

 - Go to **File > Project Settings > XR Plug-in Management**

	 -  Select  `Cardboard XR Plugin`  under  **Plug-in Providers**.

🎉 Nice! That's all the VR setup (relating to the project) that we need to do.

## 6. Unity Remote 5

Unity Remote is an app which allows you to mirror your game window in Unity unto your connected device.

It's super useful as it allows this by simply clicking Play inside Unity. it can also feedback input fron the phone to Unity for debugging - hence, no more *build and see what the code I wrote does on my phone*

To make this work:

1. Download and install the **Unity Remote 5** app on your device

2. Go to **Edit > Project Settings > Editor** and inside the **Unity Remote** group, select **Any Android/iOS device**, depending on what you build on

3. Connect your phone to the PC

4. Open Unity Remote 5

5. Open the Drone-Shooter-Scene and hit **Play**. You should see the game mirrored on your phone.

## 7. Bluetooth controller input testing

### A few words about these controllers

After using these controllers, I can say they have a somewhat **funky** behaviour. I've encountered unsolvable (as the time of writing this) incompatibilities with smartphones and they also have multiple input schemes.

❗ I could not manage to get my controller to work with an Android phone which has **Bluetooth 5.1**. I've tested with 3 other phones (all of them having **Bluetooth 5.0 or below**) and they worked flawlessly. After scouring the internet for answers, and not finding out why, I've come to to accept this limitation. It may not be BT 5.1 the issue here, but I do not have another device to test. If you'd be so kind to test with your phone and tell me about your experience, I'd be grateful!

Here's the basic layout for this type of controller

![enter image description here](https://i.imgur.com/NTe7oRY.jpg)

As you can see, you can change the input functions by holding down the **@** key and pressing **A/B/C/D**.

| Combination | Function				  |
| :---:       | :---:                     |
| **@ + A**   | Music & video mode        |
| **@ + B**   | Game mode 	              |
| **@ + C**   | VR video mode             |
| **@ + D**   | Mouse input mode          |

> ⭐ Note: I cannot guarantee these modes will work the same for your controller as I don't know if this stuff is standardized.

Also, after some testing, I've come up with the **mapping of joystick keys to Unity's expected joystick buttons**. These apply in the so-called **Game Mode**, accessed by holding **@ + B** together.

| Joystick physical key | Unity button name |
| :---:                 | :---:             |
| **A**   		        | joystick button 3 |
| **B**   		        | joystick button 0 |
| **C**   		        | joystick button 1 |
| **D**   		        | joystick button 2 |
| **L1**   		        | joystick button 4 |
| **R1**   		        | joystick button 5 |
| **Start**   		    | joystick button 10|
| **Stick X-axis**   	| Horizontal        |
| **Stick Y-axis**   	| Verical           |
| **@**   			    | Did not find any mapping|

Indeed, it makes a lot of sense 🤦🏻‍♂️.

#### Controller usage

The design of these controllers is also a bit weird. As I've seen, they should be used sideways (as the image above), with your left thumb controlling the stick and your right thumb dealing with the buttons. this is the input scheme we'll be using for this project.

This applies to the **Game Mode**. In **Mouse Mode** is should be operated with only one hand.

### Testing time, finally

- Open the scene **BT-Controller-Tester**

- Open **Unity Remote 5**, make sure phone is connected

- Pair your phone with the bluetooth controller

- After pairing is successful, set the controller in **Game Mode** by pressing **@ + B** together

- Hit Play and test out the controls! If the correct button lights up green when pressed then your controller works the same as mine. **Keep in mind** that **L1** and **R1** should not work. These will be mapped by you

### Mapping L1 and R1

- Go to **Edit > Project Settings > Input Manager**. Expand the **Axes** group and you'll see all the mapping which currently exist in this project.

- Here you can create new bindings for keyboard/mouse/controller, whatever you can connect with Unity

- For example, here's the button I created and named **Joystick Button A**

![enter image description here](https://i.imgur.com/aAzuCQd.png)

- As you can see, it maps to **joystick button 3**, this is the internal name for Unity to recognize the binding, this means that when you press he button **A** on the controller, by testing the input on the button named **Joystick Button A** you can make some action which is internally mapped to **joystick button 3**.

- **⭐ TODO**: Right-click on any binding and select **Duplicate Array Element**. Create, by refering to the table presented above, the bindings for buttons **L1 and R1**

- **⭐ TODO**: Open the script **BTControllerInputTester.cs** and complete the code in order to make the **L1 and R1** buttons green when you press them. You already have the code for the rest of the buttons so doing this should be trivial.

- When done, test the binding you created by playing the **BT-Controller-Tester** scene using Unity Remote 5 open on your smartphone.

## 8. FPS game conversion to VR

- In order to tell Unity we want to make a VR game, we need to tell it which **Camera** to setup for VR. This camera will be then used (after building the game) to track the rotation of your head.

- Go back to the **Drone-Shooter-Scene**, and find the **Main Camera** in the hierarcy, which should be under **Player**

- In the inspector, simply click on **Add Component** and add the **Tracked Pose Driver** component. This applies the current Pose value of a tracked device to the transform of the GameObject. More info here: https://docs.unity3d.com/2018.2/Documentation/ScriptReference/SpatialTracking.TrackedPoseDriver.html

- There's one more thing to do, and that is to disable the **Player Camera Rotation script**, which is attached to the **Player** GameObject.

- Go to **File > Build Settings** and add this scene to the **Scenes In Build** (only this scene!)

- **Build And Run** - now we wait for Unity to process everything (it might take a while) 

- Watch your phone as the progress bar nears completion. You'll probably have to **permit the app to install on your smartphone**

![enter image description here](https://i.imgur.com/nyDdbqY.jpg)

-  If all goes well, the app should be installed on your smartphone and it should automatically start

- Put the phone in your VR holder, reconnect the controller, if needed, and try out the game. 
	- Move around with the **Stick**, shoot balloons with button **B** and jump around with button **A** 

## 9. Hands-on work

The only difference between your build and the one seen in the demo video is that drones are completely passive in this project. You'll have to implement their **attack behaviour** and the **projectile logic**.

### 1. Drone Attack Behaviour

Follows these guidelines (also, see the behaviour in the video), but feel free to tweak the attack behaviour as you see fit 😀

- When attacked (player's baloon hits drone), the drone currently has a **25% change to aggro you**. Consult the ```TakeDamage()``` function inside the ```DroneController.cs``` script to see the implementation.

- The following functionalities should be implemented in the ```DroneAttackController.cs``` script:
	
	- The drone should stop its flight (disable the ```DroneFlightControllerScript.cs```)
	
	- The drone should face towards you
	
	- At regular time intervals (for example, every 1 second), it should shoot a projectile towards you. The actual projectile is called **DroneProjectile** and can be found under the **Prefabs** folder
	
	- Do not instantiate the **DroneProjectile** at the position of the drone, as it will automatically collide with it. Instantiate it in front of the drone, a possible solution could be ```transform.position + 2 * transform.forward```, where ```transform``` refers to the drone's transform.
	
	- When shooting, the drone should play the sound **droneFire.mp3**, found under the **Sounds** folder. As the drone has **2 AudioSources** attached, we
	-  have to select which one to play when firing (as the first sound is already used for the flying sound). I've already setup this for you, consult the ```PlayFireSound()``` inside the ```DroneAttackController.cs``` for implementation
	
	- When a **DroneProjectile** collides with the player, he/she should take damage. The ```PlayerController.cs``` scrips already has a ```TakeDamage()``` function, you can make use of that
	
#### Optional

This is the most basic attack controller. Of course, it can be imporved, for example, after the drone starts shooting and you get out of its range, it should follow you and start attacking again, if it loses sight of you, it will go to your last position, etc. Implement whatever else you fancy!

### 2. Drone Projectile Controller

- This should work similarly to the behaviour described in ```BaloonProjectileController.cs``` - study this for reference.

- ⭐ Note that the **DroneProjectile** prefab has a **Rigidbody** component attached, with **Use Gravity** disabled. This way, the projectile, when shot towards the player, will follow a straight line. Whether leave this as is, or, if you want gravity to affect this projectile as well, then you'll have to make tweaks to where the drone targets (this is up to you to figure out).

#### Happy coding!
