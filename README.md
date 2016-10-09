# Lazy Eye Therapy App (Cardboard/Daydream)

This was done for the Boston VR Hackathon (Oct 2016).
We decided to do something different and write 3 different platforms and 3 different apps and this is one of them.

This is a modified version of the HeadsetDemo from the Unity SDK.  One of the treatments for Lazy Eye (aka "amblyopia") is to force you to use your lazy eye by blurring out the image in your dominant eye and another is to show you different images in each eye so this demo does both.  There is also a button to let you switch blurred eye where the menu panel is on the floor.

# Separating Left/Right Eyes
One of the things that isn't documented with the latest Daydream SDK is how to show different images in each eye (most people won't do this) so I'm going to document it here in case anyone else needs it and doesn't want to suffer through a day of ratholes:

- first thing to note: WHAT YOU SEE IN UNITY PREVIEW MODE IS NOT THE SAME ON THE DEVICE!  That was my biggest waste of time and took most of a day.  In the Unity editor's preview, you're running the old software rendered VR that was used by Cardboard, but when you deploy to the device with the "Daydream Version of Unity", you're using Unity's native VR support which is why we needed a special build of Unity to work on Daydream apps

- duplicate the main camera that comes in the demo scenes and name them left/right
- on the left one, get rid of the GVRAudio and Audio Listeners; also, change the tag on it to Untagged instead of MainCamera
- add LeftEye and RightEye for layers 8 and 9
- for the left camera, change the culling mask so it doesn't include the RightEye layer
- for the right camera, change the culling mask so it doesn't include the LeftEye layer

That's it.  If you need to use any of the ImageEffects filters, be aware that you will probably have to turn off Direct Rendering on each camera.  ImageEffects assume you have access to the rendering pipeline in software, unfortunately.

# License
This project is mostly Apache License, with the caveat that you can't use it in a hackathon or put it up as your own app on the appstore.

Ken Yee

For the other projects, check out these github repos:

[VRCinema](https://github.com/preetishkakkar/VRCinemaHackton)
[LazyEye](https://github.com/githubildar/LazyEye)

