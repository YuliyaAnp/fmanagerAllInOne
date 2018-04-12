class Transactions extends React.Component {
    constructor(props){
        super(props);
        this.state = {
            sum: "",
            desc: "",
            date: "",
            accountFrom: "",
            accountTo: "",
            transactions: this.props.initialData,
            accounts: []
        };
        this.handleInputChange = this.handleInputChange.bind(this);
        this.getTransactionsFromServer = this.getTransactionsFromServer.bind(this);
        this.handleAddTransaction = this.handleAddTransaction.bind(this);
        this.handleAccountFromSelectChange = this.handleAccountFromSelectChange.bind(this);
        this.handleAccountToSelectChange = this.handleAccountToSelectChange.bind(this);
    }

    getTransactionsFromServer()
    {
        console.log("getTransactionFromServer");
        fetch(this.props.url, {
                        mode: 'cors',
                        cache: 'no-cache',      
                        headers : { 
                                'content-type': 'application/json',
                                'accept': 'application/json'
                              }})
         .then(response => response.json())
         .then(json => {
         console.log(json);
         this.setState({ transactions: json});
         });
    }

    componentDidMount(){
        console.log("componentDidMount");
        window.setInterval(this.getTransactionsFromServer, this.props.pollInterval);

        console.log("getAccountsFromServer");
        fetch("/accounts/Get", {
                        mode: 'cors',
                        cache: 'no-cache',      
                        headers : { 
                                'content-type': 'application/json',
                                'accept': 'application/json'
                              }})
         .then(response => response.json())
         .then(json => {
         console.log(json);
         this.setState({ accounts: json});
       });
    }

    handleInputChange(event){
      const target = event.target;
      const name = target.name;
      const value = target.value;
      this.setState({
      [name]: value
      });
    }

     handleAccountFromSelectChange(event){
       this.setState({accountFrom: event.target.value});
    }

    handleAccountToSelectChange(event){
       this.setState({accountTo: event.target.value});
    }

    handleAddTransaction(event){
        console.log("handleAddTransaction");
        event.preventDefault();
        var sum = this.state.sum.trim();
        var desc = this.state.desc.trim();
        var date = this.state.date;
        var accountTo = this.state.accountTo;
        var accountFrom = this.state.accountFrom;

        if (!sum || !desc) {
          return;
        }

        var data = {Sum: sum, 
                    Description: desc,
                    DateTime: date,
                    AccountToIncreaseAmount: accountTo,
                    AccountToSubstractAmount: accountFrom};

        fetch(this.props.postUrl, {
                        body: JSON.stringify(data), 
                        cache: 'no-cache', 
                        credentials: 'same-origin',
                        headers: {
                            'user-agent': 'Mozilla/4.0 MDN Example',
                            'content-type': 'application/json'
                                 },
                        method: 'POST', 
                        mode: 'cors', 
                        redirect: 'follow',
                        referrer: 'no-referrer', 
         });

         this.setState({sum: '', desc: '', date: '', accountFrom: '', accountTo: ''});
    }

    render() {

      return (
          <div>
            <div>
                <TableOfTransactions transactions={this.state.transactions} deleteUrl={this.props.deleteUrl}/>
            </div>
            <div>
                <ul></ul>
                <table class="add-transaction-table">
                    <tbody>
                    <tr class="add-transaction-table">
                        <td>Sum</td>
                        <td><input name="sum" type="text" placeholder="Sum" value={this.state.sum} onChange={this.handleInputChange}/></td>
                    </tr>
                    <tr class="add-transaction-table">
                        <td>Description</td>
                        <td><input name="desc" type="text" placeholder="Description" value={this.state.desc} onChange={this.handleInputChange}/></td>
                    </tr>
                    <tr class="add-transaction-table">
                        <td>Date</td>
                        <td><input name="date" type="date" placeholder="Date" value={this.state.date} onChange={this.handleInputChange}/></td>
                    </tr>
                    <tr class="add-transaction-table">
                        <td>From account</td>
                        <td>
                          <select onChange={this.handleAccountFromSelectChange}>
                                {this.state.accounts.map(function(account) 
                                    { 
                                        return <option value={account.name}>{account.name}</option>
                                    })}
                          </select>                        
                        </td>
                    </tr>
                    <tr class="add-transaction-table">
                        <td>To account</td>
                        <td>                          
                            <select onChange={this.handleAccountToSelectChange}>
                                {this.state.accounts.map(function(account) 
                                    { 
                                        return <option value={account.name}>{account.name}</option>
                                    })}
                          </select>                            
                        </td>
                    </tr>
                    </tbody>
                </table>

                <button onClick={this.handleAddTransaction}>Add Transaction</button>
            </div>
            <p></p>
          </div>
          );
  }
};

class DeleteButton extends React.Component{
    constructor(props) {
        super(props);
        this.onClick = this.onClick.bind(this);
    }

    onClick(event) {
    console.log("deleteHandleClickButton");
    fetch(this.props.dUrl+"/"+this.props.id, {
                        cache: 'no-cache', 
                        credentials: 'same-origin',
                        headers: {
                            'user-agent': 'Mozilla/4.0 MDN Example',
                            'content-type': 'application/json'
                                 },
                        method: 'DELETE', 
                        mode: 'cors', 
                        redirect: 'follow',
                        referrer: 'no-referrer', 
         });
    }

    render(){
        return(
            <button onClick={this.onClick}>Delete</button>
        )
    }
};


class TableOfTransactions extends React.Component {
    constructor(props){
        super(props);
    }

    render() {
    var cols = [
    { key: 'id', label: 'No'},
    { key: 'sum', label: 'Sum' },
    { key: 'description', label: 'Description' },
    { key: 'dateTime', label: 'Date'},
    { key: 'accountToSubstractAmount', label: 'AccountFrom'},
    { key: 'accountToIncreaseAmount', label: 'AccountTo'},
    ];
    var deleteUrl = this.props.deleteUrl;

        return (
            <div className="transactions-table">
                <h1>Transactions</h1>
                <table class="transaction-table">
                    <thead>
                    <tr>
                        <th style={{"width" : "10%"}}>{cols[0].label}</th>
                        <th style={{"width" : "10%"}}>{cols[1].label}</th>
                        <th style={{"width" : "30%"}}>{cols[2].label}</th>
                        <th style={{"width" : "20%"}}>{cols[3].label}</th>
                        <th style={{"width" : "15%"}}>{cols[4].label}</th>
                        <th style={{"width" : "15%"}}>{cols[5].label}</th>
                    </tr>
                    </thead>
                    <tbody>
                      {this.props.transactions.map(function(item) 
                        {
                            var cells = cols.map(function(colData) 
                            {
                                return <td>{item[colData.key]}</td>;
                            });
                            return <tr key={item.id}>{cells}<td>
                            <DeleteButton id={item.id} dUrl={deleteUrl}/>
                            </td></tr>;
                        })} 
                    </tbody>
                </table>
            </div>
        );
    }
};