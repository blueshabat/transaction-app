import React, { useState } from 'react';
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { Form, Button, Container, Alert } from 'react-bootstrap';

const TransactionForm = () => {
  const baseURL = "http://localhost:5230/api/Transaction/Create";
  const navigate = useNavigate();
  const [amount, setAmount] = useState('');
  const [type, setType] = useState('');
  const [description, setDescription] = useState('');
  const [category, setCategory] = useState('');
  const [date, setDate] = useState('');

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
      .post(baseURL, {
        type: Number(type === '' ? '1' : type),
        amount: Number(amount),
        date: date,
        description: description,
        category: category
      })
      .then((response) => {
        if (response.data.statusCode === 'SUC000') {
          alert("La transacción fue registrada correctamente");
          navigate("/list");
        }
        else{
          alert(response.data.message);
        }
      }).catch(error => {
        alert("Ocurrió un error al registrar la transacción" + error);
      });

  };

  const cancelHandler = () => {
    setDescription('');
    setType('');
    setAmount('');
    setCategory('');
    setDate('');
    navigate("/list");

  };
  return (
    <Alert variant='primary'>
      <Container>
        <Form onSubmit={submitActionHandler}>
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
          <Button type='submit'>Agregar transacción</Button>
          &nbsp;&nbsp;&nbsp;
          <Button type='submit' onClick={() => cancelHandler()}>Cancelar</Button>
        </Form>
      </Container>
    </Alert>

  );
};
export default TransactionForm;