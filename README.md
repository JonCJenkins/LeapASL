# LeapASL
LeapASL is a unity program that uses C# and Python to allow for seamless translation using Machine Learning and the Leap Motion Controller to convert from ASL to text to speech spoken English.

It is also intended to be a platform for future work on the subject, usable as a jumping off point for sign language interpretation work. Future work could test other sensors, different machine learning algorithms, or capture different data sets.


# Main Unity Folders

-The Scripts folder houses all of the different scripts being run in Unity, these are the brains of the program and handle everything from simply swapping scenes to the complex task of collecting all of the data from the Leap Motion Sensor and formatting it properly to be stored.

-The Scenes folder simply contains the different scenes that the program needs, there is a scene for choosing what mode you want to enter, a scene for choosing what word you want to sign, and of course a scene for actually signing.

-The Plugins folder is rather self-explanatory, simply containing the different Unity plugins that are necessary for the program to properly run. This includes the plugin for the Leap Motion Sensor, in addition to the plugin to allow the program to interface with the Windows text-to-speech ability.

-The StreamingAssets folder is where all files that are meant to be accessible through file location are stored. This includes the .csv files that house the data to train the machine learning models, the two primary python scripts, and the saved machine learning/normalization models.

# Python Scripts
The two Python scripts that are called by Unity are save.py and load.py, where save.py creates the machine learning model and then saves it, and load.py opens up server communication with Unity and allows for real time classification.

For these Python scripts to properly function Python needs to be callable by command line, and the following scripts must be installed and available to Python on the command line: zmq, pandas, io, pickle, numpy, tensorflow, keras, and sklearn.
