import React from "react";
import { Link } from 'react-router-dom';
import Spinner from '../Spinner';
import ErrorText from '../ErrorText/ErrorText';
  
import {
    Alert,
    Button,
    Card,
    CardHeader,
    CardBody,
    CardTitle,
    InputGroup,
    InputGroupAddon,
    Label,
    FormGroup,
    Input,
    Row,
    Col,
    UncontrolledAlert,
    Form,
  } from "reactstrap";
  
  class Register extends React.Component {
      state = {
          user: {
              email: "",
              username: "",
              password: "",
          }
      };

      componentDidMount() {
          this.props.resetErrors();
      };

      handleInputChange = event => {
          event.preventDefault();
          this.setState({
              [event.target.name]: event.target.value
          });
      };

      submitRegister = event => {
          const user = {
              email: this.state.email,
              username: this.state.username,
              password: this.state.password  
          };

          event.preventDefault();
          this.props.register(user);
      }

      render(){
          return(
              <div className="content">
                  <Row>
                      <Col className="ml-auto mr-auto text-center" md="4">
                          <Card>
                              <CardHeader>
                                  <CardTitle tag="h3">
                                     Zarejestruj się
                                  </CardTitle>
                              </CardHeader>
                              <CardBody>
                                  <form onSubmit={this.submitRegister} autoComplete="off">
                                      <InputGroup>
                                        <InputGroupAddon>@</InputGroupAddon>
                                        <Input
                                            defaultValue=""
                                            placeholder="email"
                                            autoComplete="off"
                                            name="email"
                                            type="email"
                                            maxLength="25"
                                            minLength="4"
                                            required
                                            onChange={event => this.handleInputChange(event)}
                                        />
                                      </InputGroup>

                                      <FormGroup>
                                          <Input 
                                            defaultValue=""
                                            placeholder="login"
                                            autoComplete="off"
                                            name="username"
                                            type="text"
                                            maxLength="25"
                                            minLength="4"
                                            required
                                            onChange={event => this.handleInputChange(event)}
                                          />
                                      </FormGroup>

                                      <FormGroup>
                                          <Input
                                             defaultValue=""
                                             placeholder="hasło"
                                             autoComplete="off"
                                             name="password"
                                             type="password"
                                             maxLength="25"
                                             minLength="4"
                                             required
                                             onChange={event => this.handleInputChange(event)}
                                          />
                                      </FormGroup>

                                      <Button color="success" className="btn-simple">Zarejestruj się</Button>
                                  </form>
                                  {!this.props.loading ? <>{this.props.response && this.props.error === null ? <><br/>
                                   <Alert color="success">
                                       Udało się zarejestrować 
                                  </Alert></> : <></>}   

                                  {this.props.error !== null ? <ErrorText error={this.props.error}></ErrorText>: <> </>} </>: <Spinner />}
                                  
                                  <br />
                                  <Label><Link to="/auth/login">Zaloguj się, </Link>jeśli masz już konto.</Label>
                              </CardBody>
                          </Card>
                      </Col>
                  </Row>
              </div>
          );
      }
  }

  export default Register;