var animationSpeed = 500;

var questionElementTop;
var questionElementLeft;
var questionElementWidth;

var currentFeatureId;
var currentFeature;
var isGuessAnimalElement;

window.onload = function() {
    var questionContainer = $('#' + feature.Id);
    questionElementLeft = questionContainer.offset().left;
    questionElementTop = "30px";
    questionElementWidth = questionContainer.width();

    currentFeature = feature;
    isGuessAnimalElement = false;
};


function processAnswer(isYes, feature) {
    if (isYes && !isGuessAnimalElement) {
        guessAnimal(feature);
    }
    else {
        currentFeature = feature;
        getNextFeature(isYes);
    }   
}

function showNextFeature() {
    removeCurrentQuestion(currentFeature.Id);
    if (isGuessAnimalElement) {
        removeCurrentQuestion('animalGuess');
        isGuessAnimalElement = false;
    }
    createQuestion(feature);
    showElement(feature.Id);
}

function createQuestion(feature) {
    var questionElement = '<div class="question-container hidden-left" id="' + feature.Id + '">';
    questionElement += '<h2 class="text-center">' + feature.Text + '?</h2>';
    questionElement += '<div class="button-container">';
    questionElement += '<div class="btn btn-primary btn-large question-button" onclick="processAnswer(true, feature);">Yes</div>';
    questionElement += '<div class="btn btn-primary btn-large question-button" onclick="processAnswer(false, feature);">No</div>';
    questionElement += '</div>';
    questionElement += '</div>';
    $('#main-container').append(questionElement);
}

function guessAnimal(feature) {
    isGuessAnimalElement = true;
    removeCurrentQuestion(feature.Id);
    setQuestion(feature);
    showElement('animalGuess');
    
}

function setQuestion(feature) {
    var questionElement = $('#animalGuessQuestion');
    questionElement.text("Is it " + feature.Animal.Title + "?");
}

function removeCurrentQuestion(featureId) {

    var questionContainer = $('#' + featureId);

    questionContainer.css("position", "absolute");
    questionContainer.css("width", questionElementWidth);
    questionContainer.css("left", questionElementLeft);
    questionContainer.css("top", questionElementTop);
    
    questionContainer.css({left: questionElementLeft}).animate({ left: questionElementLeft + 1000 }, animationSpeed);
    questionContainer.animate({opacity: "0.5"}, animationSpeed, function() {
        questionContainer.css("display", "none");
    });
}

function showElement(elementId) {
    var quessAnimalElement = $('#' + elementId);

    quessAnimalElement.css("display", "block");
    quessAnimalElement.css("top", questionElementTop);
    quessAnimalElement.css("width", questionElementWidth);
    
    quessAnimalElement.css({ left: questionElementLeft - 1000 }).animate({ left: questionElementLeft }, animationSpeed);
    quessAnimalElement.animate({ opacity: "1" }, animationSpeed, function () {   
    });
}

function showWinElement() {
    removeCurrentQuestion('animalGuess');
    showElement('winElement');
}

function showFailElement() {
    removeCurrentQuestion(feature.Id);
    if (isGuessAnimalElement) {
        removeCurrentQuestion('animalGuess');
    }
    showElement('failElement');
}