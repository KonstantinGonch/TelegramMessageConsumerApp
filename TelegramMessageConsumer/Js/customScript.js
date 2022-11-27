function getSelectionParentElement() {
    var parentEl = null, sel;
    if (window.getSelection) {
        sel = window.getSelection();
        if (sel.rangeCount) {
            parentEl = sel.getRangeAt(0).commonAncestorContainer;
            if (parentEl.nodeType != 1) {
                parentEl = parentEl.parentNode;
            }
        }
    } else if ((sel = document.selection) && sel.type != "Control") {
        parentEl = sel.createRange().parentElement();
    }
    return parentEl;
}

function getCommentContent(parentElement) {
    let messageContainer = parentElement.querySelector('.message');
    return messageContainer.childNodes[0].nodeValue;
}

function getCommentUsername(parentElement) {
    let nameContainer = parentElement.querySelector('.name');
    return nameContainer.textContent;
}

function getCommentPostedDate(parentElement) {
    return parentElement.getAttribute("data-timestamp")
}

function getCommentIsResponse(parentElement) {
    return parentElement.classList.contains("is-reply");
}

window.onmouseup = event => {
    event.preventDefault();
    let selection = document.getSelection();
    let selectedText = selection.toString();
    if (selectedText.trim() != '') {
        let parentEl = getSelectionParentElement();
        while (!parentEl.hasAttribute("data-mid")) {
            parentEl = parentEl.parentNode;
        }
        if (confirm('Сохранить сообщение?')) {
            let jsonObject =
            {
                content: getCommentContent(parentEl),
                username: getCommentUsername(parentEl),
                postedDate: getCommentPostedDate(parentEl),
                isResponse: getCommentIsResponse(parentEl),
            };
            window.chrome.webview.postMessage(jsonObject);
        } else {
        }
    }
}