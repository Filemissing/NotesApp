const title = document.getElementById("note-title");
const input = document.getElementById("markdown-input");
const preview = document.getElementById("markdown-preview");

let saveTimeout;

if (title && input && preview) {

    title.addEventListener("input", () => {

        updateSidebarTitle();

        contentChanged();

    });
    input.addEventListener("input", contentChanged);
}

async function updatePreview() {

    const response = await fetch(
        "?handler=Preview",
        {
            method: "POST",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify({
                content: input.value
            })
        }
    );


    preview.innerHTML = await response.text();

}

async function saveNote() {
    setStatus("Saving...");

    const response = await fetch(
        "?handler=Save",
        {
            method: "POST",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify({
                noteId: getNoteId(),
                title: title.value.trim() || "Untitled Note",
                content: input.value
            })
        }
    );


    if (response.ok) {
        console.log("Saved");
        setStatus("Saved");
    }
}

function contentChanged() {
    updatePreview();

    clearTimeout(saveTimeout);

    saveTimeout = setTimeout(() => {
        saveNote();
    }, 1000);
}

function updateSidebarTitle() {
    const noteId = getNoteId();

    const link = document.getElementById(
        "note-link-" + noteId
    );


    if (link) {
        const titleElement = link.querySelector(
            ".note-title-display"
        );

        titleElement.textContent =
            title.value;
    }
}

function getNoteId() {
    return document.getElementById("note-id").value;
}

function setStatus(text) {
    document.getElementById("save-status").textContent = text;
}