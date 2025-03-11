import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import { Form, Button, Container, Alert } from 'react-bootstrap';

const TransactionForm = () => {

  const editURL = "http://localhost:5230/api/Transaction";
  const navigate = useNavigate();
  const param = useParams();
  const [transactionId, setTransactionId] = useState('');
  const [amount, setAmount] = useState('');
  const [type, setType] = useState('');
  const [description, setDescription] = useState('');
  const [category, setCategory] = useState('');
  const [date, setDate] = useState('');

  useEffect(() => {

    axios.post(editURL + '/GetById', { transactionId: param.id }).then((response) => {
      if (response.data.statusCode === 'SUC000') {
        const transaction = response.data.data;
        setTransactionId(transaction.id);
        setAmount(transaction.amount);
        setType(transaction.type === 'Ingreso' ? '1' : '0');
        setDescription(transaction.description);
        setCategory(transaction.category);
        setDate(transaction.date.substring(0, 10));
      }
    }).catch(error => {
      alert("Ocurrió un error al obtener los datos de la transacción " + error);
    });
  }, []);

  const typeChangeHandler = (event) => {
    setType(event.target.value);
  };

  const descriptionChangeHandler = (event) => {
    setDescription(event.target.value);
  };

  const categoryChangeHandler = (event) => {
    setCategory(event.target.value);
  };

  const dateChangeHandler = (event) => {
    setDate(event.target.value);
  };

  const amountChangeHandler = (event) => {
    const result = event.target.value.replace(/\D/g, '');
    setAmount(result);
  };

  const submitActionHandler = (event) => {
    event.preventDefault();
    axios
      .put(editURL + '/Update', {
        id: transactionId,
        type: Number(type === '' ? '1' : type),
        amount: Number(amount),
        date: date,
        description: description,
        category: category
      })
      .then((response) => {
        if (response.data.statusCode === 'SUC000') {
          alert("La transacción fue actualizada correctamente");
          navigate('/list');
        }
        else {
          alert(response.data.message);
        }
      }).catch(error => {
        alert("Ocurrió un error al actualizar la transacción" + error);
      });

  };

  return (
    <Alert variant='primary'>
      <Container>
        <Form onSubmit={submitActionHandler} id="data">
          <Form.Group controlId="form.id">
            <Form.Label>Id</Form.Label>
            <Form.Control value={transactionId} readonly='readonly' />
          </Form.Group>
          <Form.Group controlId="form.Type">
            <Form.Label>Tipo</Form.Label>
            <Form.Select onChange={typeChangeHandler} value={type}>
              <option disabled>Selecciona un tipo</option>
              <option value="1">Ingreso</option>
              <option value="0">Gasto</option>
            </Form.Select>
          </Form.Group>
          <Form.Group controlId="form.Amount">
            <Form.Label>Monto</Form.Label>
            <Form.Control type="text" value={amount} onChange={amountChangeHandler} required />
          </Form.Group>
          <Form.Group controlId="form.Date">
            <Form.Label>Fecha</Form.Label>
            <Form.Control type="date" value={date} onChange={dateChangeHandler} required />
          </Form.Group>
          <Form.Group controlId="form.Description">
            <Form.Label>Descripción</Form.Label>
            <Form.Control type="text" value={description} onChange={descriptionChangeHandler} required />
          </Form.Group>
          <Form.Group controlId="form.Category">
            <Form.Label>Categoría</Form.Label>
            <Form.Control type="text" value={category} onChange={categoryChangeHandler} required />
          </Form.Group>
          <br></br>
          <Button type='submit'>Actualizar transacción</Button>
          &nbsp;&nbsp;&nbsp;
          <Button type='submit' onClick={() => navigate("/list")}>Cancelar</Button>
        </Form>
      </Container>
    </Alert>

  );
};
export default TransactionForm;
