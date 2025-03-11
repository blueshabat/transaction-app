import React from 'react';
import {Routes,Route,Navigate} from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import AddTransaction from './components/AddTransaction';
import EditTransaction from './components/EditTransaction';
import TransactionDataTable from './components/TransactionDataTable';

function App() { 

  return (
   
    <div  class="container card mb-4 box-shadow">
    
        <div class="card-header">
            <h4 class="my-0 font-weight-normal">Transacciones</h4>
          </div>

    <Routes>
        <Route path="/" element={<Navigate to="/list" />} />
        <Route exact path="/create" element={<AddTransaction/>}/>
        <Route exact path="/list" element={<TransactionDataTable/>}/>
        <Route path="/edit/:id" element={<EditTransaction/>}/>
      </Routes>

    </div>
  );
}

export default App;
