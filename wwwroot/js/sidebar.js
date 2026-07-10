document.addEventListener("DOMContentLoaded", () => {

    const button = document.getElementById("projects-collapse-button");
    const list = document.getElementById("projects-list");
    const icon = document.getElementById("projects-collapse-icon");

    if (!button || !list || !icon)
        return;

    button.addEventListener("click", () => {

        const collapsed = list.style.display === "none";

        if (collapsed) {
            list.style.display = "flex";
            icon.classList.remove("bi-chevron-right");
            icon.classList.add("bi-chevron-down");
        }
        else {
            list.style.display = "none";
            icon.classList.remove("bi-chevron-down");
            icon.classList.add("bi-chevron-right");
        }

    });

});