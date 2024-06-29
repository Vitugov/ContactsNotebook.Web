
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