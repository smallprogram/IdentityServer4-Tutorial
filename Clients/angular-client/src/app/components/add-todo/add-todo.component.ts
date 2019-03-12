import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { TodoService } from 'src/app/services/todo.service';
import { MatSnackBar } from '@angular/material';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-todo',
  templateUrl: './add-todo.component.html',
  styleUrls: ['./add-todo.component.css']
})
export class AddTodoComponent {
  addressForm = this.fb.group({
    title: [null, Validators.required],
  });

  hasUnitNumber = false;

  constructor(private fb: FormBuilder,
    private todoServer : TodoService,
    private snckbar:MatSnackBar,
    private router:Router)
    { }

  onSubmit() {
    if(this.addressForm.valid){
      const todo = this.addressForm.value;
      this.todoServer.addTodo(todo).subscribe(tb => {
        this.snckbar.open("添加成功了！",'Close',{duration :5000});
        this.router.navigate(['/todo']);
      })
    }
  }
}
