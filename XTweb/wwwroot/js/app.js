const chatInput = document.querySelector(".chat-input textarea");
const sendChatBtn = document.querySelector(".chat-input i");
const chatBox = document.querySelector(".chatbox");
const chatbotToggler = document.querySelector(".chatbot-toggler");
const chatbotCloseBtn = document.querySelector(".close-btn");


let userMessage;
const API_KEY = "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";

const createChatLi = (message, classname) => {
    const chatLi = document.createElement("li");
    chatLi.classList.add("chat", classname)
    let chatContent = classname === "outcoming" ? `<p></p>` : `<i class="fi fi-rr-robot"></i><p></p>`;
    chatLi.innerHTML = chatContent;
    chatLi.querySelector("p").textContent = message;
    return chatLi;
}

const generateResponse = (incomingChatLi) => {
    const API_URL = "https://api.openai.com/v1/chat/completions";
    const messageElement = incomingChatLi.querySelector("p");
    const requestOptions = {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${API_KEY}`
        },
        body: JSON.stringify({
            model: "gpt-3.5-turbo-0125",
            messages: [
                {
                    role: "user",
                    content: userMessage
                }
            ]
        })
    }

    fetch(API_URL, requestOptions).then(res => res.json()).then(data => {
        messageElement.textContent = data.choices[0].message.content;
    }).catch((error) => {
        messageElement.classList.add("error")
        messageElement.textContent = "Có một vài lỗi sảy ra, vui lòng nhập lại!";
    }).finally(() => chatBox.scrollTo(0, chatBox.scrollHeight))
}

const handleChat = () => {
    userMessage = chatInput.value.trim();
    if (!userMessage) return;
    chatInput.value = "";

    chatBox.appendChild(createChatLi(userMessage, "outcoming"));

    chatBox.scrollTo(0, chatBox.scrollHeight);

    setTimeout(() => {
        const incomingChatLi = createChatLi("Thinking...", "incoming");
        chatBox.appendChild(incomingChatLi);
        chatBox.scrollTo(0, chatBox.scrollHeight);
        generateResponse(incomingChatLi);
    }, 600);
}
chatbotCloseBtn.addEventListener("click", () => document.body.classList.remove("show-chatbot"));
sendChatBtn.addEventListener("click", handleChat)
chatbotToggler.addEventListener("click", () => document.body.classList.toggle("show-chatbot"));
