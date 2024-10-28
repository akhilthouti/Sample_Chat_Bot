// wwwroot/js/script.js

async function sendMessage() {
    const userInput = document.getElementById("user-input").value;
    if (!userInput) return;

    // Display the user's message
    addMessageToChat("You", userInput);

    // Send the message to the backend
    try {
        const response = await fetch("/send-message", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ message: userInput })
        });

        if (!response.ok) {
            throw new Error("Error fetching chatbot response");
        }

        const data = await response.json();
        addMessageToChat("Bot", data.botResponse);
    } catch (error) {
        addMessageToChat("Bot", "Sorry, something went wrong.");
        console.error(error);
    }

    // Clear the input field
    document.getElementById("user-input").value = "";
}

// Function to add messages to the chat output
function addMessageToChat(sender, message) {
    const chatOutput = document.getElementById("chat-output");

    const messageElement = document.createElement("div");
    messageElement.classList.add("message");

    if (sender === "You") {
        messageElement.innerHTML = `<strong>${sender}:</strong> ${message}`;
        messageElement.style.textAlign = "right";
        messageElement.style.color = "#007bff";
    } else {
        const formattedMessage = message.split("\n").map(line => `<p>${line}</p>`).join("");
        messageElement.innerHTML = `<strong>${sender}:</strong> ${message}`;
        messageElement.style.textAlign = "left";
        messageElement.style.color = "#333";
    }

    chatOutput.appendChild(messageElement);
    chatOutput.scrollTop = chatOutput.scrollHeight;
}
