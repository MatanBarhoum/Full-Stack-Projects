import { ThisReceiver } from '@angular/compiler';
import { Component, OnInit, Input } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
@Component({
  selector: 'app-add-edit-emp',
  templateUrl: './add-edit-emp.component.html',
  styleUrls: ['./add-edit-emp.component.css']
})
export class AddEditEmpComponent implements OnInit {
  constructor(private Service:SharedService) { }

  @Input() emp:any;
  EmployeeId:string;
  EmployeeName:string;
  Department:string;
  DateOfJoining:string;
  ngOnInit(): void {
    this.EmployeeId=this.emp.EmployeeId;
    this.EmployeeName=this.emp.EmployeeName;
    this.Department=this.emp.Department;
    this.DateOfJoining=this.emp.DateOfJoining;;
  }

  updateEmployee() {
    var val = {EmployeeId:this.EmployeeId,
      Department:this.Department,
      EmployeeName:this.EmployeeName,
      DateOfJoining:this.DateOfJoining};
    this.Service.updateEmployee(val).subscribe(res=>{
      alert(res.toString());
    })
  }

  addEmployee() {
    var val = {EmployeeId:this.EmployeeId,
      EmployeeName:this.EmployeeName,
      Department:this.Department,
      DateOfJoining:this.DateOfJoining};
    this.Service.addEmployee(val).subscribe(res=>{
    alert(res.toString());
  })
}
}

