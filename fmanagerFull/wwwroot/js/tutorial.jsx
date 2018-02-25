/*import React, { Component } from 'react';
import './App.css'; 
import PropTypes from 'prop-types'; */

var App = React.createClass( {
    constructor: function(props){
        super(props);
        this.state = {
            sum: 0,
            desc: "",
            transactions:
                [
                    {Sum: -10, Description: "Food"},
                    {Sum: 200, Description: "Income"},
                    {Sum: -30, Description: "Cinema"},
                ]
        };
        this.handleInputChange = this.handleInputChange.bind(this);
    }

    handleInputChange(event){
      const target = event.target;
      const name = target.name;
      const value = target.value;
      this.setState({
      [name]: value
      });
    }

    render: function() {
      return (
          <div>
              <div>
                <TableOfTransactions
                transactions = {this.state.transactions}/>
            </div>
            <div>
                <ul></ul>
                <input name="sum" type="text" placeholder="Sum" onChange={this.handleInputChange}/>
                <input name="desc" type="text" placeholder="Description" onChange={this.handleInputChange}/>
                <button onClick={() => {this.setState(
                                        {transactions: [...this.state.transactions, {Sum: this.state.sum, Description: this.state.desc}]
                                        })}}>Add Transaction</button>
            </div>
          </div>
          );
  }
});

/*
App.propTypes = {
    transactions: PropTypes.arrayOf(PropTypes.shape({
        sum: PropTypes.number.isRequired,
        description: PropTypes.string.isRequired
    })),
} */

var TableOfTransactions = React.createClass( {
    renderTransaction: function(i)
    {
        return (
            <tr>
                <td>{i+1}</td>
                <td>{this.props.transactions[i].Sum}</td>
                <td>{this.props.transactions[i].Description}</td>
            </tr>
        );
    }
    render: function() {
        return (
            <div className="transactions-table">
                <h1>Transactions</h1>
                <table>
                    <tr>
                        <th style={{"width" : "20%"}}>No</th>
                        <th style={{"width" : "30%"}}>Sum</th>
                        <th style={{"width" : "50%"}}>Description</th>
                    </tr>
                    {this.props.transactions.map( (tr, index) => (this.renderTransaction(index)) )}
                </table>
            </div>
        )
    }
});



ReactDOM.render(<App />, document.getElementById('root'));