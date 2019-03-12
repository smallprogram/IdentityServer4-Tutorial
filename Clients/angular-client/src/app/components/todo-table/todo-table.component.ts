import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { TodoTableDataSource } from './todo-table-datasource';
import { TodoService } from 'src/app/services/todo.service';
import { ITodo } from 'src/app/Models/todo';

@Component({
  selector: 'app-todo-table',
  templateUrl: './todo-table.component.html',
  styleUrls: ['./todo-table.component.css']
})
export class TodoTableComponent implements OnInit {
  // @ViewChild(MatPaginator) paginator: MatPaginator;
  // @ViewChild(MatSort) sort: MatSort;


  dataSource: TodoTableDataSource;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['id', 'title', 'completed'];

  todos: ITodo[];
  constructor(private todoServer:TodoService){}


  ngOnInit() {
    // this.dataSource = new TodoTableDataSource(this.paginator, this.sort);
    this.todoServer.getAllTodos().subscribe(todos => {
      this.todos = todos;
      console.log(todos);
    });
  }
}
