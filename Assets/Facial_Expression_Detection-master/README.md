# Facial_Expression_Detection
### Term Project semester 3(submodule) : Trained model

In this program, we are not training any machine learning model or CNN. The Expression Detection model is trained using Google's TensorFlow and Keras python package. The trained Keras trained model is available in */models* folder as model.h5.

In this python program, we are implementing that trained model to detect facial expressions.

## Requirements :
Python 3.7 is recommended for this program.

All the required packages are available in requirement.txt file in the root directory.
Use the following command to install the requirements :

pip install -r requirement.txt

or

pip3 install -r requirement.txt

or

### you can install all the packages manually using pip:

  -tensorflow==2.1.0
  
  -keras==2.3.1
  
  -opencv-python==4.1.2
  
  -matplotlib==3.1.3
  


**You can feed image to the program in two ways :**

1. Put the image in the *image/* directory 
2. Using webcam (*Press spacebar to snap an image*)

If you are putting the image in the directory manually that you need to feed the name of the image to the main.py.

**To run the program:**

1. Install all the requirements(Using a virtual environment is recommended).
2. Clone this respiratory and cd into the root directory.
3. Run main.py by : python main.py or python3 main.py

### Reference :

1. https://github.com/MauryaRitesh/Facial-Expression-Detection-V2 (training model)
2. https://keras.io/ (keras docs)
3. https://opencv-python-tutroals.readthedocs.io/en/latest/py_tutorials/py_gui/py_image_display/py_image_display.html (opencv image operations)
4. https://subscription.packtpub.com/book/application_development/9781785283932/3/ch03lvl1sec28/accessing-the-webcam (webcam access using opencv)
