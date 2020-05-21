import React from 'react';

import { UncontrolledAlert } from 'reactstrap';



const ErrorText = props => {
    return (
        props.error !== null ? <> <br/> <br/>
        <UncontrolledAlert color="danger">
            <span>
                {props.error.error}
            </span>
        </UncontrolledAlert>
        </> : <></>
    );
}

export default ErrorText;


