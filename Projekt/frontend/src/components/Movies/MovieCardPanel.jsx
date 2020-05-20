import React from 'react';
import MovieCard from './MovieCard'

class MovieCardPanel extends React.Component {
    render () {
        let movies = null;

        if (this.props.movies) {
            movies = this.props.movies.map(movie => {
                return (
                    <MovieCard
                        key={movie.id}
                        id={movie.id}
                        movieName={movie.name}
                        movieDescription={movie.movieDescription}
                        movieImage={movie.logo}
                        isAuthenticated={this.props.isAuthenticated}
                    />
                );
            })
        }

        return(
            <>
                {movies}
            </>
        );
    }
}

export default MovieCardPanel;
