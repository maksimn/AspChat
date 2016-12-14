import React from "react";
import { Link } from "react-router";

export default class RegisterPage extends React.Component {
    render() {
        return (
            <h1>Страница регистрации <Link to="/">Назад</Link></h1>
        );
    }
}