import { combineReducers } from 'redux';
import { routerReducer } from 'react-router-redux';

const scriptElement = document.querySelector('#main');      
const jsonChatMessages = scriptElement.getAttribute('data-chat-messages');

let initChatMessages = JSON.parse(jsonChatMessages);
initChatMessages = initChatMessages != null 
    ? 
        initChatMessages.map((cm) => { return {
            chatUserName: cm.ChatUserName,
            chatMessageText: cm.Text
        }}) 
    : 
        []
;

let userName = scriptElement.getAttribute('data-username');
userName = userName == null ? "" : userName;
console.log("userName=",userName);

function userNameReducer(state=userName, action) {
    return state;
}

function chatMessagesReducer(state=initChatMessages, action) {
    if (action.type === "CHAT_MESSAGE_RECEIVED") {
        const newId = state.length;
        const newChatMessage = {
            chatMessageId: newId,
            chatUserName: action.payload.chatUserName,
            chatMessageText: action.payload.chatMessageText
        }
        return [...state, newChatMessage];
    }
    return state;
}

const reducers = combineReducers({
    userName: userNameReducer,
    chatMessages: chatMessagesReducer,
    routing: routerReducer
});

export default reducers;
