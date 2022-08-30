This package is free to use in any project. However you may not redistribute it as if it were your own.

You do not have to, but i would appreciate if you were to give credit to my work.

[How to use]
In a preload scene or in the current scene, make sure you have an instance of the AudioConfigManager placed in it.
	This script will never be destroyed unless another instance exists (Only 1 instance may exist)
	It ensures all subsequent scripts will work

On you audio source, place the AudioSourceType script on it, then select the type for the source.

It should now update the volume of the source based on the settings.

[How to modify the audio types]
Find the AudioTypes asset, and add/remove any text objects in the list, making sure that all entries are unique.

[How to adjust settings at runtime]
Please reffer to the demo provided for this. It contains a example script that handles just that.