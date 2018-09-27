import React,{Component} from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as userManagementAction from '../actions/userMgrAction';


class Home extends Component
{
  render(){

    const {user} = this.props;
    if(Object.keys(user).length === 0 && user.constructor === Object){
      return(
        <div>
          <h1>Welcome Pandalogix</h1>
          <p>Your serverless logic app made easy!</p>
          
          <div>
            <a className="btn btn-primary" onClick={this.login}>Login/SignUp</a>
          </div>
        </div>
      );
    }
    if(user.isNew){
      this.props.history.push('/profile/id');
    
      
    }else{
      this.props.history.push('/dashboard');
    }
    return (
      <div>
        {user.name}
      </div>
    );
    
  } 
  
  login=()=>{
    if(!this.props.user.name)
    this.props.actions.login();
  }
} 


function mapStateToProps(state, ownProps) {
  var user = state.UserManager;

  return {
    user: user
  };
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(userManagementAction, dispatch)
  };
}

export default connect(mapStateToProps,mapDispatchToProps)(Home);