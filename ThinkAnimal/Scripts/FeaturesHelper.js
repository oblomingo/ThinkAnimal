//animation speed for slide effect
var animationSpeed = 500;

//animation offset for slide effect
var animationOffset = 1500;

//Params to set question element position
var questionElementTop;
var questionElementLeft;
var questionElementWidth;

//Current feature, we will save feature here when we get next feature
var currentFeatureId;
var currentFeature;

//Flag to control question logic
//if it's guess animal question then we will get next feature
var isGuessAnimalElement;

window.onload = function() {
    var questionContainer = $('#' + feature.Id);
    questionElementLeft = questionContainer.offset().left;
    questionElementTop = "30px";
    questionElementWidth = questionContainer.width();

    currentFeature = feature;
    isGuessAnimalElement = false;
};

//Get nex feature and show it, else show fail message
function getNextFeature(childFeatureId) {

    $.ajax({
        url: '/Home/GetFeatureById/' + childFeatureId,
        type: 'GET',
        success: function (nextFeature) {
            if (nextFeature) {
                feature = nextFeature;
                showNextFeature();
            }

        }
    });
}

//Process player answer after Yes/No selection
function processAnswer(isYes, feature) {
    if (isYes && !isGuessAnimalElement) {
        guessAnimal(feature);
    }
    else {
        //Save current feature and get a new one
        currentFeature = feature;
        var featureId = getNextFeatureId(feature, isYes);
        if (featureId)
            //if we have next(child) element then get and show it
            getNextFeature(featureId);
        else {
            //if no, just show fail message
            showFailElement();
        }
    }   
}

//Get next(child) feature for yes or no answers
function getNextFeatureId(feature, isYes) {
    if (isYes && feature.ChildFeatureForYes != null)
        return feature.ChildFeatureForYes.Id;
    if (!isYes && feature.ChildFeatureForNo != null)
        return feature.ChildFeatureForNo.Id;
    return false;
}


//Show next question about feature
function showNextFeature() {
    //Remove question
    removeCurrentQuestion(currentFeature.Id);
    if (isGuessAnimalElement) {
        removeCurrentQuestion('animalGuess');
        isGuessAnimalElement = false;
    }
    createQuestionElement(feature);
    showElement(feature.Id);
}

//Create question element with text about feature
function createQuestionElement(feature) {
    var questionElement = '<div class="question-container hidden-left" id="' + feature.Id + '">';
    questionElement += '<h2 class="text-center">' + feature.Text + '?</h2>';
    questionElement += '<div class="button-container">';
    questionElement += '<div class="btn btn-primary btn-large question-button" onclick="processAnswer(true, feature);">Yes</div>';
    questionElement += '<div class="btn btn-primary btn-large question-button" onclick="processAnswer(false, feature);">No</div>';
    questionElement += '</div>';
    questionElement += '</div>';
    $('#main-container').append(questionElement);
}

//guess animal like: "Is it bear?"
function guessAnimal(feature) {
    isGuessAnimalElement = true;
    removeCurrentQuestion(feature.Id);
    setQuestion(feature);
    showElement('animalGuess');    
}

//Set question text to element
function setQuestion(feature) {
    var questionElement = $('#animalGuessQuestion');
    questionElement.text("Is it " + feature.Animal.Title + "?");
}

//Remove current question element with animation
function removeCurrentQuestion(featureId) {

    var questionContainer = $('#' + featureId);

    questionContainer.css("position", "absolute");
    questionContainer.css("width", questionElementWidth);
    questionContainer.css("left", questionElementLeft);
    questionContainer.css("top", questionElementTop);
    
    questionContainer.css({ left: questionElementLeft }).animate({ left: questionElementLeft + animationOffset }, animationSpeed);
    questionContainer.animate({opacity: "0.5"}, animationSpeed, function() {
        questionContainer.css("display", "none");
    });
}

//Show question or win/fail message with animation
function showElement(elementId) {
    var quessAnimalElement = $('#' + elementId);

    quessAnimalElement.css("display", "block");
    quessAnimalElement.css("top", questionElementTop);
    quessAnimalElement.css("width", questionElementWidth);
    
    quessAnimalElement.css({ left: questionElementLeft - animationOffset }).animate({ left: questionElementLeft }, animationSpeed);
    quessAnimalElement.animate({ opacity: "1" }, animationSpeed, function () {   
    });
}

//Show win message, end of game
function showWinElement() {
    removeCurrentQuestion('animalGuess');
    showElement('winElement');
}

//Show fail message, end of game
function showFailElement() {
    removeCurrentQuestion(feature.Id);
    if (isGuessAnimalElement) {
        removeCurrentQuestion('animalGuess');
    }
    showElement('failElement');
}