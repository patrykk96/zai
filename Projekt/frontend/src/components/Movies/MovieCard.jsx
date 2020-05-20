import React from 'react';
import Rating from 'react-rating';
import "./MovieCard.css";

import {
    Card,
    Col,
    CardHeader,
    CardBody,
    CardTitle,
    Label,
    CardImg
} from 'reactstrap';

class MovieCard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            rating: null
        };
    }

    render() {

        return (
            <Col lg="4">
                <Card>
                    <CardHeader>
                        <h5 className="card-category">Test</h5>
                        <CardTitle tag="h3">
                            <i className="tim-icons icon-controller text-info" /> {" "}
                            {this.props.movieName}
                        </CardTitle>
                        <a href="#">
                            <CardImg src={this.props.movieImage} className="customCardImg"></CardImg>
                        </a>
                    </CardHeader>
                    <CardBody>
                        <Label>
                            {this.props.movieDescription}
                        </Label>
                        <br />
                        <Label>
                            Średnia ocen:
                            <br />
                            <Rating
                                placeholderRating={this.props.movieRating}
                                emptySymbol="tim-icons icon-shape-start rating"
                                fullSymbol="tim-icons icon-shape-star text-success rating"
                                placeholderSymbol="tim-icons icons-shape-star text-success rating"
                                readonly 
                            />
                        </Label>
                        <br />
                    </CardBody>
                </Card>
            </Col>
        );
    }
}

export default MovieCard;