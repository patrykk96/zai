import React from "react";

import {
  Button,
  Card,
  CardHeader,
  CardBody,
  CardTitle,
  Table
} from "reactstrap";

class MoviePanel extends React.Component {
    constructor(props){
        super(props);
        this.state = {
            enabledEdit: false,
            selectedId: null,
            image: null,
            name: null,
            description: null
        }
    }

    render() {
        return (
            <>
                <Card className="card-tasks">
                    <CardHeader>
                        <CardTitle tag="h3">
                            <i className="tim-icons icon-controller text-info" /> {" "}
                            Baza filmów

                            <Button onClick={this.props.toggleMovieAdd} className="float-right" color="primary">
                                Dodaj film
                            </Button>
                        </CardTitle>
                    </CardHeader>
                    <CardBody>
                        <Table>
                            <thead className="text-primary">
                                <tr>
                                    <th>Obraz</th>
                                    <th>Tytuł</th>
                                    <th>Opis</th>
                                    <th>Średnia ocen</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>

                            </tbody>
                        </Table>
                    </CardBody>
                </Card>
            </>
        );
    }
}

export default MoviePanel;