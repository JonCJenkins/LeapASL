"""
import Orange
import pickle

dataFile = "HandData.csv" 
data = Orange.data.Table(dataFile);
learner = Orange.classification.NNClassificationLearner(max_iter=500)
cf = learner(data)
cf.instances = data

with open("model.pkcls", "wb") as f:
    pickle.dump(cf, f)
"""

import pandas as pd
import numpy as np
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import LabelEncoder
from keras.models import Sequential
from keras.layers import Dense

classifier = Sequential()

classifier.add(Dense(37,input_dim=37,activation='relu'))
classifier.add(Dense(13, activation='softmax'))

classifier.compile(optimizer = 'adam', loss = 'categorical_crossentropy', metrics = ['accuracy'])

data = pd.read_csv("HandData.csv")
data = data.iloc[2:]

TFDict = {
    'False':0,
    'True':1
    }

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

data = data.fillna(0)

x = data.iloc[:,1:40].values
y = data.iloc[:,0].values

le = LabelEncoder()
le.fit(y)
y = le.transform(y)

from keras.utils.np_utils import to_categorical
y = to_categorical(y, num_classes=None)

x_train, x_test, y_train, y_test = train_test_split(x, y, test_size=0.33, random_state=1)

from sklearn.preprocessing import StandardScaler
sc = StandardScaler()
sc.fit(x)
x_train = sc.transform(x_train)
x_test = sc.transform(x_test)

classifier.fit(x_train,y_train,batch_size=32,epochs=100)

y_pred = classifier.predict(x_test)

pred = pd.DataFrame(np.argmax(y_pred, axis=1))
test = pd.DataFrame(np.argmax(y_test, axis=1))

from sklearn.metrics import confusion_matrix
cm = confusion_matrix(test,pred)

import pickle
pickle.dump(sc,open('StandardScalar.sav','wb'))
pickle.dump(le,open('LabelEncoder.sav','wb'))
classifier.save('ANNModel')