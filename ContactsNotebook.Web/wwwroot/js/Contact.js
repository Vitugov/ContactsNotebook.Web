
function toggleEditMode() {
    const editableElements = document.querySelectorAll('.editable');
    const submitButton = document.querySelector('.editable-btn');
    const editToggleButton = document.getElementById('editToggle');

    editableElements.forEach(element => {
        element.disabled = false;
    });

    submitButton.disabled = false;
    editToggleButton.style.display = 'none';
}

function toggleEditModeInitial(isEditable) {
    const editableElements = document.querySelectorAll('.editable');
    const submitButton = document.querySelector('.editable-btn');
    const editToggleButton = document.getElementById('editToggle');

    editableElements.forEach(element => {
        element.disabled = !isEditable;
    });

    submitButton.disabled = !isEditable;
    if (isEditable) {
        editToggleButton.style.display = 'none';
    }
}

function submitForm(requestMethod, route) {
    const form = document.getElementById("contactForm");
    const formData = new FormData(form);

    const data = {};
    formData.forEach((value, key) => {
        data[key] = value;
    });

    const xhr = new XMLHttpRequest();
    const url = route;

    xhr.open(requestMethod, url, true);

    xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");

    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE) {
            if (xhr.status >= 200 && xhr.status < 300) {
                const response = JSON.parse(xhr.responseText);
                if (response.redirectToUrl) {
                    window.location.href = response.redirectToUrl;
                } else {
                    console.log('Success:', response);
                }
            } else if (xhr.status === 405) {
                // Обработка ошибки, например, перерисовка представления
                document.body.innerHTML = xhr.responseText;
            } else {
                console.error('Error:', xhr.statusText);
            }
        }
    };

    xhr.send(JSON.stringify(data));
}

//function submitForm(requestMethod, route) {
//    const form = document.getElementById("contactForm");
//    const formData = new FormData(form);
//    const data = {};
//    formData.forEach((value, key) => {
//        data[key] = value;
//    });
//    console.log('Body:', JSON.stringify(data));
//    fetch(route, {
//        method: requestMethod,
//        headers: {
//            "Content-Type": "application/json"
//        },
//        body: JSON.stringify(data)
//    });
//}