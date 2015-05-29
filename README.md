# AltspaceVR Programming Project - Unity Cursor

## Instructions

Finish the implementation of the 3D cursor in an example Unity application, and then build some enhancements using the cursor.

## Goals

We use this test to get a sense of your coding style and to how you creatively solve both a concrete problem and an abstract one. When we receive your project, here is what we will be asking ourselves:

- Is their cursor correct? Does it look and act like the provided example build?

- Do the enhancements implemented work well?

- Are the enhancements creative, challenging to implement, and just plain cool?

- Is the code well structured, easy to read and understand, and organized well?

This project should take approximately 5-15 hours to complete. To work on the project:

- Fork and clone the repo.
- Open up the Unity project in `Project`, and work there. This project was built in Unity 4.6, so if you are using Unity 5 you may need to migrate the project or [download and install 4.6](https://unity3d.com/get-unity/download/archive).

# Part 1 - 3D Cursor (3-5 hours)

If you’ve tried AltspaceVR, you’ll have noticed that our user interface uses a 3D cursor. This cursor approach allows the user to select objects in the scene, or interact with 2D web panels. For this part of the project, you’ll be implementing a variant of the 3D cursor algorithm we’ve developed for AltspaceVR. You can find an example build showing the expected behavior in the `Builds` folder of the repo.

The example project has most everything you need **except** for the logic to drive the cursor off of the mouse, and the shader for the cursor. The script you will need to implement is the `SphericalCursorModule.cs` script that is on the `Main Camera` under the `First Person Controller`.

The cursor is represented by a sphere, found in the GameObject `CursorMesh` under the `Cursor` GameObject under the camera. The job of the `SphericalCursorModule` script is to update the position and scale of the cursor GameObject based upon the movement of the mouse and a raycast to find which object the cursor is over.



Here are the defining features of the cursor algorithm that you should replicate:

- The state of the cursor is represented as spherical coordinates on a sphere surrounding the player. So, as the user moves their mouse, you should be updating the coordinates of the cursor in this space.

- Each frame, a raycast from the eye is made based on the spherical coordinates, against objects in a layer mask of all the selectable objects in the scene. It this project, layer #8 contains all the objects. The proper raycast mask is defined for you in the `ColliderMask` field in the code. (Note that this scene just has simple box colliders, so your actual collision points may not lie on the objects' surfaces.)
  - If there is no collision, the cursor geometry should be scaled to the DefaultCursorScale and positioned on the surface of a large virtual sphere of radius SphereRadius surrounding the player.
  - If there **is** a collision, the cursor geometry should be positioned at the hit point and scaled uniformly based upon the distance to the hit, using the equation:
    - `(distanceToObject * DistanceScaleFactor + 1.0f) / 2.0f`
      - `distanceToObject`: Distance to the hit point
      - `DistanceScaleFactor`: Tuning factor, set on the script properties panel

  By scaling the cursor this way, you get a nice scale effect where it doesn’t feel quite like a real sphere in the scene, but a flat 2d cursor that gets slightly smaller with distance.

  This approach works well in VR. The cursor is always either sitting on the surface of an object, or it is far away on a virtual sphere, so there are no convergence issues. (For example, having the cursor float in free space near the player’s head is a sure way to cause discomfort.)

- The diffuse shader included by default on the cursor will cause the cursor to appear like a normal sphere which will be depth sorted and shaded based upon lights. You should write and attach a custom shader that does two things:
  - Draws the cursor on top of all other geometry.
  - Draws the cursor as flat and bright, so it appears more like a circle than a sphere. (Hint: you should ignore lights in the scene.)

- Sensitivity of the cursor to mouse movement should be adjustable via the `Sensitivity` property on the script.

For this part of the project, please **do not** include 3rd party code. You can reference 3rd party code of course, but any code you write for the cursor should be your own. (We'll be asking you how it works!)

# Part 2 - Enhancements (5-10 hours)

Now that you have a working cursor, and can point at and select objects, now build some functionality that might be fun in VR that showcases your skills and creativity. This is the open ended part of the project, and is your chance to blow us away! 

Some potential ideas:

- Use the cursor to manipulate objects, and add multiplayer!

- Improve the cursor to be more usable.

- Add some custom shaders, behaviors, or effects to objects to show off your graphics chops.

- Add a control scheme or widgets that let the user do stuff with objects.

- Create some kind of interactive game.

- Anything you want! Got some new Unity technique you want to try? Use this as an excuse! Don’t feel limited by the sample scene, feel free to create a new scene and leverage your cursor code in some novel way.

Feel free to use 3rd party code or assets for this part of the project, keeping in mind our assessment criteria (noted at the top of the README.)

## Deliverable

In your repo, you should clobber this README file with your own describing your project. Any instructions or known issues should be documented in the README as well.

E-mail us a link to your Github repo to `projects@altvr.com`. Please include your contact information, and if you haven't submitted it to us already, your resume and cover letter. 

We hope you have fun working on the project, and we can't wait to see what you come up with!
    
[The Altspace Team](http://altvr.com/team/)
    
##Acknowledgements

*Assets used in this project are from* [Free Furniture Props](https://www.assetstore.unity3d.com/en/#!/content/8822)


