class App extends React.Component {
    constructor(props){
        super(props);
        this.state = {
            sum: 0,
            desc: "",
            transactions:[]
        };
        this.handleInputChange = this.handleInputChange.bind(this);
        this.getTransactionsFromServer = this.getTransactionsFromServer.bind(this);
        this.handleAddTransaction = this.handleAddTransaction.bind(this);
    }

    getTransactionsFromServer()
    {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function() 
        {
            var data = JSON.parse(xhr.responseText);
            this.setState({ transactions: data });
        }.bind(this);
        xhr.send();
    }

    componentDidMount(){
    this.getTransactionsFromServer();
    window.setInterval(this.getTransactionsFromServer, this.props.pollInterval);
    }

    handleInputChange(event){
      const target = event.target;
      const name = target.name;
      const value = target.value;
      this.setState({
      [name]: value
      });
    }

    handleAddTransaction(event){
        event.preventDefault();
        var sum = this.state.sum.trim();
        var desc = this.state.desc.trim();
        if (!sum || !desc) {
          return;
        }
        // TODO: send request to the server
        var data = new FormData();
        data.append('sum', this.state.sum);
        data.append('description', this.state.desc);

        var xhr = new XMLHttpRequest();
        xhr.open('post', this.props.postUrl, true);
        xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        xhr.onload = function() {
          this.getTransactionsFromServer();
        }.bind(this);
        xhr.send(data);

    this.setState({sum: '', desc: ''});
    }

    render() {
      return (
          <div>
              <div>
                <TableOfTransactions
                transactions = {this.state.transactions}/>
            </div>
            <div>
                <ul></ul>
                <input name="sum" type="text" placeholder="Sum" value={this.state.sum} onChange={this.handleInputChange}/>
                <input name="desc" type="text" placeholder="Description" value={this.state.desc} onChange={this.handleInputChange}/>
                <button onClick={this.handleAddTransaction}>Add Transaction</button>
            </div>
          </div>
          );
  }
};

/*App.propTypes = {
    transactions: PropTypes.arrayOf(PropTypes.shape({
        sum: PropTypes.number.isRequired,
        description: PropTypes.string.isRequired
    })),
}*/

class TableOfTransactions extends React.Component {

    render() {

    var cols = [
    { key: 'id', label: 'No'},
    { key: 'sum', label: 'Sum' },
    { key: 'description', label: 'Description' }
    ];

        return (
            <div className="transactions-table">
                <h1>Transactions</h1>
                <table>
                    <thead>
                    <tr>
                        <th style={{"width" : "20%"}}>{cols[0].label}</th>
                        <th style={{"width" : "30%"}}>{cols[1].label}</th>
                        <th style={{"width" : "50%"}}>{cols[2].label}</th>
                    </tr>
                    </thead>
                    <tbody>
                        {this.props.transactions.map(function(item) 
                        {
                            var cells = cols.map(function(colData) 
                            {
                                return <td>{item[colData.key]}</td>;
                            });
                            return <tr key={item.id}>{cells}</tr>;
                        })}
                    </tbody>
                </table>
            </div>
        );
    }
};

ReactDOM.render(<App url="/api/transactions" postUrl="/api/transactions" pollInterval={2000}/>, document.getElementById('root'));