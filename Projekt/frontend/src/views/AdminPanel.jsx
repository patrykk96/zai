import React from 'react';
import { connect } from 'react-redux';

import MovieAdd from 'components/AdminPanel/MovieAdd';
import MoviePanel from 'components/AdminPanel/MoviePanel';
import Spinner from 'components/Spinner'
import * as movieActions from 'store/actions/movieActions';

import {
    Row,
    Col,
} from 'reactstrap';

class AdminPanel extends React.Component {
    constructor(props){
        super(props);
        this.state = {
            toggleMovieAdd: false
        };
    }

    toggleMovieAdd = () => {
        this.setState(prevState => ({
            toggleMovieAdd: !prevState.toggleMovieAdd
        }));
    }

    render(){
        return(
            <>
                <div className="content">
                    <Row>
                        {this.props.loading ? <Spinner/> : 
                         <Col className="ml-auto mr-auto" lg="12" md="12">
                             {this.state.toggleMovieAdd ? 
                                <MovieAdd
                                    movieAdd={this.props.movieAdd}
                                    toggleMovieAdd={this.toggleMovieAdd} 
                                />
                                :
                                <MoviePanel
                                    toggleMovieAdd={this.toggleMovieAdd}
                                />
                             }
                         </Col>
                        }
                    </Row>
                </div>
            </>
        );
    }
}

const mapDispatchToProps = dispatch => {
    return {
      movieAdd: (movie) => dispatch(movieActions.movieAdd(movie))
    };
};

const mapStateToProps = state => {
    return {
        loading: state.movieReducer.loading,
        error: state.movieReducer.error
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(AdminPanel);
