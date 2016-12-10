import React from "react";
import ChatMessageView from "../ChatMessageView";
import styles from "./ChatMessagesListView.css";

export default class ChatMessagesListView extends React.Component {
    constructor() {
        super();
        this.isScrolledFromBottomPos = false;
        this.messagesDiv = undefined;
    }

    componentDidMount() {
        this.messagesDiv = document.querySelector("." + styles.chatMessagesListView);
        this.updateScroll();
        setInterval(this.updateScroll.bind(this), 300);
    }

    updateScroll() {
        let thisDiv = this.messagesDiv;
        if (!(this.isScrolledFromBottomPos)) {
            thisDiv.scrollTop = thisDiv.scrollHeight;
        }
    }

    scrollHandler(e) {
        let thisDiv = e.target;
        this.isScrolledFromBottomPos = thisDiv.clientHeight + thisDiv.scrollTop !== thisDiv.scrollHeight;
    }

    render() {
        const { appState } = this.props;
        let { chatMessages } = appState;
        
        let chatMessageViews = [];
        for(let i = 0; i < chatMessages.length; i++) {
            const chatMessage = chatMessages[i];
            chatMessageViews.push(
                <ChatMessageView
                    key={i}
                    ChatMessageId={chatMessage.chatMessageId}
                    ChatUserName={chatMessage.chatUserName}
                    >
                    {chatMessage.chatMessageText}
                </ChatMessageView>
            );
        }
        return (
            <div className={ styles.chatMessagesListView }
                 onScroll={ this.scrollHandler.bind(this) }>
                {chatMessageViews}
            </div>
        );
    }
}
