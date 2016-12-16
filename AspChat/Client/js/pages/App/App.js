import React from "react";
import styles from "./App.css";

export default class App extends React.Component {
    render() {
        return (
            <div>
                <header className={ styles.header }>
                    <div className={ styles.chatLabel }>Чат</div>
                </header>
                {this.props.children}
            </div>
        );
    }
}
