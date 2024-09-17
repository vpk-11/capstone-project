import { useEffect, useState } from 'react';
import './List.css'

const List = () => {

  const [record, setRecord] = useState([])
  // const [modeldata, setModeldata] = useState({
  //   id: '',
  //   userName: '',
  //   username: '',
  //   email: '',
  //   website: ''
  // })

  const getData = () => {
    fetch('https://jsonplaceholder.typicode.com/users/')
      .then(resposne => resposne.json())
      .then(res => setRecord(res))
  }

  useEffect(() => { getData(); }, [])

  const showDetail = (id) => {
    fetch(`https://jsonplaceholder.typicode.com/users/${id}`)
      .then(resposne => resposne.json())
      // .then(res => setModeldata(res))
  }

  return (
    <div className='container'>
      <h3>Check out our MBS pool collection</h3>
      <table className='table-container'>
        <thead>
          <tr>
            <th>No</th>
            <th>Name</th>
            <th>Username</th>
            <th>Email</th>
            <th>Website</th>
            <th>Show Details</th>
          </tr>
        </thead>
        <tbody>
          {record.map((names, index) =>
            <tr classNam="row-container" key={index}>
              <td>{names.id}</td>
              <td>{names.name}</td>
              <td>{names.username}</td>
              <td>{names.email}</td>
              <td>{names.website}</td>
              <td><button class="btn btn-primary" onClick={(e) => showDetail(names.id)} data-toggle="modal" data-target="#myModal">Get Details</button></td>
            </tr>
          )}
        </tbody>
      </table>
      <span className='container'>Hello World</span>
    </div>
  )
}

export default List;