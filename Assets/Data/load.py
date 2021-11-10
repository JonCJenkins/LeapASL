"""
import Orange
import pickle
import zmq

context = zmq.Context()
socket = context.socket(zmq.REP)
socket.bind("tcp://*:5555")

with open("model.pkcls", "rb") as f:
    loadCF = pickle.load(f)
cf = loadCF

data = Orange.data.Table("HandData")
c_values = data.domain.class_var.values

while True:
    d = socket.recv()
    d = d.decode(encoding='utf-8')
    print("Got Data")
    fd = open('GuessDataActive.csv','r').readlines()
    fd[-1] = d
    open('GuessDataActive.csv', 'w').writelines(fd)
    test = Orange.data.Table("GuessDataActive.csv")
    d = test[len(test)-1]
    c = cf(d)
    guess = c_values[int(cf(d))]
    print("Sending Guess: " + guess)
    socket.send_string(guess)


print("Unexpectedly exited loop")
"""

import zmq
import pandas as pd
import io
import pickle
import numpy as np
from tensorflow import keras
print("Importing Model")
model = keras.models.load_model("ANNModel")
print("Connecting to Unity")
context = zmq.Context()
socket = context.socket(zmq.REP)
socket.bind("tcp://*:5555")
print("Entering Loop")
col = ["LeftX",
       "LeftY",
       "LeftZ",
       "LeftThumb",
       "LeftIndex",
       "LeftMiddle",
       "LeftRing",
       "LeftPinky",
       "leftSpeed",
       "leftConf",
       "leftPitch",
       "LeftYaw",
       "LeftRoll",
       "leftThumbExt",
       "leftIndexExt",
       "leftMiddleExt",
       "leftRingExt",
       "leftPinkyExt",
       "RightX",
       "RightY",
       "RightZ",
       "RightThumb",
       "RightIndex",
       "RightMiddle",
       "RightRing",
       "RightPinky",
       "rightSpeed",
       "rightConf",
       "rightPitch",
       "rightYaw",
       "rightRoll",
       "rightThumbExt",
       "rightIndexExt",
       "rightMiddleExt",
       "rightRingExt",
       "rightPinkyExt",
       "HandDist"]

TFDict = {
    'False':0,
    'True':1,
    True:1,
    False:0
    }

sc = pickle.load(open('StandardScalar.sav','rb'))
le = pickle.load(open('LabelEncoder.sav','rb'))

while True:
    d = socket.recv()
    d = d.decode(encoding='utf-8')
    d = d[1:]
    csv = io.StringIO(d)
    data = pd.read_csv(csv, sep=",",header=None, names=col)
    data["leftThumbExt"] = data["leftThumbExt"].map(TFDict)
    data["leftIndexExt"] = data["leftIndexExt"].map(TFDict)
    data["leftMiddleExt"] = data["leftMiddleExt"].map(TFDict)
    data["leftRingExt"] = data["leftRingExt"].map(TFDict)
    data["leftPinkyExt"] = data["leftPinkyExt"].map(TFDict)
    data["rightThumbExt"] = data["rightThumbExt"].map(TFDict)
    data["rightIndexExt"] = data["rightIndexExt"].map(TFDict)
    data["rightMiddleExt"] = data["rightMiddleExt"].map(TFDict)
    data["rightRingExt"] = data["rightRingExt"].map(TFDict)
    data["rightPinkyExt"] = data["rightPinkyExt"].map(TFDict)
    x_guess = data.iloc[:,:].values
    x_guess = sc.transform(x_guess)
    y_pred = model.predict(x_guess)
    pred = pd.DataFrame(np.argmax(y_pred, axis=1))
    guess = le.inverse_transform(pred.values.ravel())
    guess = str(guess[0])
    print("Sending Guess:",guess)
    socket.send_string(guess)