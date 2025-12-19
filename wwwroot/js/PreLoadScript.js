


function toastHandle(toastType, toastMessage) {

    let waiting = setInterval(() => {

        if (document.querySelector("#ToastSuccess")) {
            let toastSucess = document.querySelector("#ToastSuccess")
            let toastError = document.querySelector("#ToastError")

            switch (toastType) {

                case "error":
                    document.querySelector("#ToastErrorMessage").innerText = toastMessage
                    gsap.to(toastError, {
                        display: "flex",
                        right: "20px"
                    })
                    setTimeout(() => {
                        gsap.to(toastError, {
                            right: "-300px",
                            display: "none"

                        })
                    }, 3000);
                    break;

                case "success":

                    document.querySelector("#ToastSuccessMessage").innerText = toastMessage
                    gsap.to(toastSucess, {
                        display: "flex",
                        right: "20px"
                    })
                    setTimeout(() => {
                        gsap.to(toastSucess, {
                            right: "-300px",
                            display: "none"
                        })
                    }, 3000);
                    break;
            }
            clearInterval(waiting)

        }
    }, 1000);

}

function ExpandNote(e) {
    document.getElementById("ExpandNote").classList.replace("hidden", "flex")
    let targetNote = e.currentTarget.closest(".note")
    let TargetNoteHead = targetNote.querySelector(".NoteHead").innerText
    let TargetNotePara = targetNote.querySelector(".NotePara").innerText
    let ExpandNoteHead = document.querySelector("#ExpandNoteHeading")
    let ExpandNotePara = document.querySelector("#ExpandNotePara")

    ExpandNoteHead.innerText = TargetNoteHead
    ExpandNotePara.innerText = TargetNotePara
}
function closeExpendNote() {
    document.getElementById("ExpandNote").classList.replace("flex", "hidden")
}