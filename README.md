# BumbleFormula: AI for Predicting Nigerian Women's Behavior on the Dating App Bumble

## Overview
**BumbleFormula** is a sophisticated AI model designed to predict the behavior of Nigerian women on the dating app Bumble based solely on their publicly available data. Leveraging the FastTree machine learning algorithm, it categorizes women into different behavioral classes based on features like age, physical activity,number of profile pictures, number of lines of bio,height, education, drinking and smoking habits among other public profile data.

As the rate of marriage is plummeting globally and many men are increasingly abandoning traditional dating avenues, this AI software presents a potential solution to make the dating landscape easier for men and less traumatic, as they are usually the ones taking the initiative at the start of a potential relationship.

**Note:** This machine learning model powering this software and the training data are both **not publicly available** to prevent any potential misuse or inappropriate applications. The model was trained with strict ethical guidelines and was developed purely for academic and research purposes which would later be published and referenced here.

## Future Plans
There are plans to convert more categorical features into numerical values for model training in version 2.0(the current version), using the level of scantiness in different aspects of the dating profiles. This means that the AI will start considering the specific kinds of information a Nigerian woman chooses to share and what she omits as predictors of her behavior. This feature will allow the model to become even more nuanced and accurate in its predictions.

[BumbleFormula Video Demonstration on Youtube](https://youtu.be/-RD6vXAnp8c?si=q8WALweURyQjr_YG)

## Diagram of the Brain of the AI based on FastTree Algorithm

![model](https://github.com/user-attachments/assets/f52787b4-ec10-423e-b4ef-99f56a01920e)

## Performance
The AI boasts a **macro accuracy of 89%** (now 91% in this version 2.0), validated through **5-fold cross-validation**. It offers predictions for various classifications of women, with results that adjust based on the scoring assigned to each class. Predictions include 15 categories such as "serious," "friend," "Gamer," and more, depending on the input data.

The AI may classify a woman into up to 4 classes, especially when her public profile information is scanty, indicating the level of difficulty in classifying her behavior accurately. To make predictions, the software prompts users with a series of questions about the woman’s dating profile. The answers are then sent to the machine learning model to make predictions, along with a percentage score for each prediction, so users can gauge the AI's confidence level in making those classifications.

## Legal Compliance
**BumbleFormula** adheres to Nigerian data protection regulation (NDPR) and privacy laws. **Multiple legal consultations** were sought from experienced lawyers in Nigeria to ensure full compliance with Nigerian data laws. This guarantees that the data used to train the AI is properly handled and respects the privacy rights of individuals.

## Key Features
- **Input Data**: Publicly available information from dating profiles.
- **Classification**: The model scores behavior predictions across multiple classes, outputting only classes where predictions score over a defined threshold.
- **Accuracy**: 89% macro accuracy(now 91%), cross-validated.
- **Dynamic Classification**: Capable of classifying women into up to 4 classes when profile information is limited, highlighting classification challenges.
- **Interactive Profiling**: Asks questions about the woman’s dating profile and uses the responses to make predictions and probabilities, with confidence scores provided for each prediction.

  ![Screenshot 2024-09-10 034331](https://github.com/user-attachments/assets/d6dd2021-e0f5-476f-a001-95c2d47e3fd1)


## Responsible Use
This AI is intended to be used for **ethical, academic, and research purposes only**. Given the sensitive nature of predicting behavior, it is crucial that this tool be used responsibly to avoid any harmful implications or invasion of privacy. We have made the decision to **keep the machine learning model and training data private** to ensure it is not used in ways that could exploit or manipulate individuals.

[BumbleFormula Explanation on Kaggle](https://www.kaggle.com/code/adedapoadeniran/human-behavior-predictor-ai)
