// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var tweetArea = document.getElementById('tweet-area');

window.onload = checkTweetLength();


function checkTweetLength() {
    if (tweetArea != null) {
        var textArea = tweetArea.value.length;
        var charsLeft = 144 - textArea;
        var count = document.getElementById('charCount');
        count.innerHTML = charsLeft;
    }
}

if (tweetArea != null) {
    tweetArea.addEventListener('keyup', checkTweetLength, false);
    tweetArea.addEventListener('keydown', checkTweetLength, false);
}
