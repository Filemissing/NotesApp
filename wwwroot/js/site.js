document.addEventListener("DOMContentLoaded", () => {

    const editButtons = document.querySelectorAll(".task-edit-button");


    editButtons.forEach(button => {

        button.addEventListener("click", () => {

            document.getElementById("edit-task-id").value =
                button.dataset.taskId;

            document.getElementById("edit-task-title").value =
                button.dataset.taskTitle;

            document.getElementById("edit-task-description").value =
                button.dataset.taskDescription ?? "";

            document.getElementById("edit-task-tags").value =
                button.dataset.taskTags ?? "";
        });

    });

});

document.getElementById("add-task-button")
    .addEventListener("click", async () => {

        const token = document.querySelector(
            'input[name="__RequestVerificationToken"]'
        ).value;


        const response = await fetch(
            `?handler=CreateEmptyTask&projectId=${projectId}`,
            {
                method: "POST",

                headers: {
                    "RequestVerificationToken": token
                }
            }
        );

        const data = await response.json();

        openTaskEditor(data.id);
    });

function openTaskEditor(taskId) {

    document.getElementById("edit-task-id").value = taskId;

    document.getElementById("edit-task-title").value = "New Task";

    document.getElementById("edit-task-description").value = "";

    document.getElementById("edit-task-tags").value = "";


    const modal = new bootstrap.Modal(
        document.getElementById("editTaskModal")
    );

    modal.show();
}

document.getElementById("cancel-edit-task")
    .addEventListener("click", async () => {

        const taskId = document.getElementById("edit-task-id").value;


        const token = document.querySelector(
            'input[name="__RequestVerificationToken"]'
        ).value;


        await fetch(
            `?handler=DeleteDraft&taskId=${taskId}`,
            {
                method: "POST",

                headers: {
                    "RequestVerificationToken": token
                }
            }
        );


        location.reload();

    });