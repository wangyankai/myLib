Getting Started with Bitmap Font Pro

Preface

This document explains how to use Bitmap Font Pro. You can find this tutorial as well as many others on the official Bitmap Font Pro website:
"bmfpro.fun4mobile.de". A video tutorial is also available at "http://www.youtube.com/watch?v=oMbFaUx6OPY".


Changelog
 - Version 1.5 (2014-07-01)
    - Stability improvements

 - Version 1.4 (2014-06-09)
    - Performance improvements, code refactoring
    - Bugfix for labels when starting/stopping in Unity editor

 - Version 1.3 (2014-06-06)
    - Bugfix for behavior when animating laben properties on devices

 - Version 1.2 (2014-06-04)
    - Added additional methods to modify labels when running on devices

 - Version 1.1 (2014-06-29)
    - Added new Method SetFont to label to change font at runtime
    - Labels now react to font changes in the editor and update immediately
    - Bugfix: Fixed data loss bug when changing label text during runtime


Introduction

Fonts are (obviously) a very important part of any game. They are used as captions on GUI controls, as score- or health-popups, for high score lists, in messages shown to the user, for item names and so on. It is also very important, that the fonts match the style of your game. If they don’t blend in your whole game scene may look unappealing and unprofessional.
Creating good looking bitmap fonts is actually not that hard. But getting them into your Unity based game is. That’s where Bitmap Font Pro comes in to save the day.


Importing the Bitmap Font Pro Package

Bitmap font pro comes as a standard Unity package. Just double click it or import it right from the Unity editor. The package consists of two parts: Bitmap Font Pro itself and a demo scene with an example font all set up.
So go ahead and import the Bitmap Font Pro into a Unity project.
The Demo Scene
After importing the package, open the demo scene. Here you should see a label saying “Hello World!”. So far so good. Feel free to play around with the label. E.g. change the text and scale it.
Now let’s add another label to the scene. Drag the BitmapFontLabel prefab into the scene. Drag the BitmapFont game object, that already is in the scene, onto the Font property of your newly created label. Also enter some text into the Text property of the label. This way you can create as many labels as you like from a single font.


The Bitmap Font Pro Workflow

When working with Bitmap Font pro you need only two prefabs:
 - Bitmap Font: holds a font, including the texture and (optionally) a retina texture. A game object of this prefab is required once per scene for every different font you would like to use.
 - Bitmap Font Label: this prefab actually shows text in your scene. It references a Bitmap Font object, from which it gets its texture and other information. You can place as many Bitmap Font Labels in your scene as you like.

The workflow for creating Bitmap Font Labels from scratch is this:
 - Create one or more fonts with a graphics program (see this tutorial)
 - This will give you a .png and a .fnt file for each font (two, if you want to use retina textures, too). Place these files in your Unity project’s assets folder.
 - Drag the BitmapFont prefab into your scene (once for each different font you want to use).
 - Assign the .png and .fnt files to the Bitmap Font game objects.
 - Drag as many BitmapFontLabel prefabs as you like into your game scene.
 - Assign one of your BitmapFont game objects to each of the labels.
 - Set your labels’ texts, positions and scaling as you like.


Typographic Features

Bitmap Font Pro supports a bunch of typographic features. Each of them can be accessed via the corresponding properties in the Unity-Inspector. Of course they can be changed programmatically while your game is running, too.
·         Text: Sets the text that is being shown by the label. The text can have multiple lines, too. To use multiple lines just copy a piece of text from a text editor into the Unity Inspector.
·         Scale: Changes the overall size of the label.
·         Line Spacing Scale: Controls how much space will be inserted between the label’s lines.
·         Horizontal align: Sets how the label will be aligned on the x-axis with regard to its position.
·         Vertical align: Sets how the label will be aligned on the y-axis with regard to its position.
·         Color: The color of the label. Usually it is recommended to set the color via the font texture. Additionally this setting can be used to fade the label in/out or tint it.


What’s Next?

If you want to learn more please visit "bmfpro.fun4mobile.de".




Bitmap Font Pro, (C) Boris Brock Softwareentwicklung, 2014
