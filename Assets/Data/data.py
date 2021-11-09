import Orange
from Orange.widgets.visualize.owruleviewer import OWRuleViewer
from AnyQt.QtWidgets import QApplication
from Orange.classification import NaiveBayesLearner

data = Orange.data.Table("HandData")

learner = Orange.classification.NNClassificationLearner(max_iter=500)

cv = Orange.evaluation.CrossValidation(k=5)

res = cv(data, [learner])

print("Accuracy: %.3f" % Orange.evaluation.scoring.CA(res)[0])
print("AUC:      %.3f" % Orange.evaluation.scoring.AUC(res)[0])


test = Orange.data.Table("GuessData")

classifier = learner(data)
c_values = data.domain.class_var.values
for d in test[5:len(test)]:
    c = classifier(d)
    #print("{}, originally {}".format(c_values[int(classifier(d))], d.get_class()))
    print(c_values[int(classifier(d))])

