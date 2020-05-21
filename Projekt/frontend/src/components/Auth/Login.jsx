import React from "react";
import { Link, Redirect } from "react-router-dom";
import Spinner from "../Spinner"
import ErrorText from '../ErrorText/ErrorText';

import {
    Button,
    Card,
    CardHeader,
    CardBody,
    CardTitle,
    Label,
    FormGroup,
    Input,
    Row,
    Col,
    UncontrolledAlert
} from "reactstrap";

class Login extends React.Component {
    constructor(props){
        super(props);
        this.state = {
            user: {
                username: "",
                password: "",
            }
        };
    };

    componentDidMount() {
        this.props.resetErrors();
    };

    componentWillUnmount() {
        if (this.props.error){
            window.location.reload();
        }
    };

    handleInputChange = event => {
        event.preventDefault();
        this.setState({
            [event.target.name]: event.target.value
        });
    };

    submitLogin = event => {
        const user ={
            username: this.state.username,
            password: this.state.password
        };
        event.preventDefault();
        this.props.login(user);
    };

    render() { 
        if (this.props.isAuthenticated) {
            return <Redirect to="/" />
        }
        return(
            <div className="content">
                <Row>
                    <Col className="ml-auto mr-auto text-center" md="4">
                        <Card>
                            <CardHeader>
                                <CardTitle tag="h3">
                                    Zaloguj się
                                </CardTitle>
                            </CardHeader>
                            <Label>Podaj login i hasło </Label>
                            <CardBody>
                                <form onSubmit={this.submitLogin}>
                                    <FormGroup>
                                        <Input
                                            name="username"
                                            defaultValue=""
                                            placeholder="Login"
                                            autoComplete="off"
                                            type="text"
                                            maxLength="25"
                                            minLength="4"
                                            required
                                            onChange={event => this.handleInputChange(event)}
                                            />
                                    </FormGroup>

                                    <FormGroup>
                                        <Input
                                            name="password"
                                            defaultValue=""
                                            placeholder="Hasło"
                                            autoComplete="off"
                                            type="password"
                                            maxLength="25"
                                            minLength="4"
                                            required
                                            onChange={event => this.handleInputChange(event)} />
                                    </FormGroup>

                                    <Button color="info" className="btn-simple">Zaloguj</Button>
                                    {this.props.loading ? <Spinner /> : <> </>}

                                    <ErrorText error={this.props.error}></ErrorText>
                                </form>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
            </div>
        );
    }
};

export default Login;
