import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import axios from "axios";
import editIcon from "./../assets/edit.png";
import deleteIcon from "./../assets/delete.JPG";
import "../App.css";


const TransactionDataTable = () => {

  const navigate = useNavigate();
  const baseURL = "http://localhost:5230";
  const [transactions, setTransactions] = useState([]);

  const setTransactionsData = () => {
    axios.get(baseURL + "/api/Transaction/List").then((response) => {
      setTransactions(response.data.data);
    }).catch(error => {
      alert("Ocurrió un error al traer los datos " + error);
    });
  };

  useEffect(() => {
    setTransactionsData();
  }, []);


  const removeTransaction = (id) => {
    axios.delete(baseURL + "/api/Transaction/Delete", { data: {transactionId: id} }).then((response) => {
      alert("La transacción fue eliminada correctamente");
      setTransactionsData();
      navigate('/list');

    }).catch(error => {
      alert("Ocurrió un error al eliminar la transacción" + error);
    });
  };

  return (
    <div class="card-body">
      <br>
      </br>
      <nav>
        <button
          className="btn btn-primary nav-item active"
          onClick={() => navigate("/create")}>
          Crear nueva transacción
        </button>
      </nav>


      <br></br>
      <div className="col-md-6">
        <h4>Últimas transacciones</h4>

        <div class="container">
          <div class="row">
            <div class="col-12">
              <table class="table table-bordered table-striped">
                <thead>
                  <tr>
                    <th>Tipo</th>
                    <th>Monto</th>
                    <th>Fecha</th>
                    <th>Descripción</th>
                    <th>Categoría</th>
                    <th>Saldo</th>
                    <th scope="col">Acciones</th>
                  </tr>
                </thead>
                <tbody>
                  {
                    transactions &&
                    transactions.map((transaction, index) => (
                      <tr style={transaction.type == 'Ingreso' ? { backgroundColor: '#a1f19c' } : { backgroundColor: '#eb8282' }}>
                        <th scope="row">{transaction.type}</th>
                        <td>{transaction.amount}</td>
                        <td>{new Intl.DateTimeFormat("en-GB", {
                          year: "numeric",
                          month: "numeric",
                          day: "numeric",
                          hour: "numeric",
                          minute: "numeric",
                          second: "numeric",
                          hour12: false,
                        }).format(new Date(transaction.date))}</td>
                        <td>{transaction.description}</td>
                        <td>{transaction.category}</td>
                        <td>{transaction.balance}</td>
                        <td >
                          <Link to={"/edit/" + transaction.id}><img src={editIcon} alt="Edit" width="50" height="30" title="Editar transacción" />
                          </Link>
                          <button
                            onClick={() => removeTransaction(transaction.id)} className="button"
                          > <img src={deleteIcon} alt="Remove" title="Eliminar transacción" width="30" height="30" />
                          </button>
                        </td>
                      </tr>
                    ))
                  }
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
export default TransactionDataTable;