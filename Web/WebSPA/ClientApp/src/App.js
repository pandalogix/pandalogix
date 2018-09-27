import React from 'react';
import { Route } from 'react-router';
import Home from './components/Home';
import Dashboard from './components/Dashboard';
import UserProfile from './components/UserProfile';
// import ProtectedRoute from './protectedRoute';


export default () => (
  <div>
    <Route exact path='/' component={Home} />
    <Route path='/dashboard' component={Dashboard} />
    <Route path='/profile/:userid' component={UserProfile} />
    {/* <Route path='/counter' component={Counter} />
    <Route path='/fetchdata/:startDateIndex?' component={FetchData} /> */}
  </div>
);

