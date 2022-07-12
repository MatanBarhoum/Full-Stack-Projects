import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-emp',
  templateUrl: './show-emp.component.html',
  styleUrls: ['./show-emp.component.css']
})
export class ShowEmpComponent implements OnInit {

  constructor(private Service:SharedService) { }

  EmployeeList:any=[];

  ModalTitle: string;
  ActivateAddEditempComp:boolean;
  emp:any;
  EmployeeId:string;
  EmployeeName:string;
  Department:string;
  DateOfJoining:string;
  ngOnInit(): void {
    this.refreshempList();
    this.EmployeeId=this.emp.EmployeeId;
    this.EmployeeName=this.emp.EmployeeName;
    this.Department=this.emp.Department;
    this.DateOfJoining=this.emp.DateOfJoining;
  }

  addClick() {
    this.emp={
      EmployeeId:0,
      EmployeeName:""
    }
    this.ModalTitle="Add Department";
    this.ActivateAddEditempComp=true;
  }

  closeClick() {
    this.ActivateAddEditempComp=false;
    this.refreshempList();
  }
  
  editClick(item:any) {
    this.emp = item;
    //this.dep.DepartmentId = item.DepartmentId;
    this.ModalTitle="Edit Employee";
    this.ActivateAddEditempComp=true;
  }

  deleteClick(item:any) {
    this.emp = item;
    var val = this.emp.EmployeeId; 
    this.Service.deleteEmployee(val).subscribe(result=>{
      alert(result.toString());
      this.refreshempList();
    })
  }

  refreshempList(){ //Async opertaion
    this.Service.getEmpList().subscribe(data=>{
      this.EmployeeList=data;
    });
  }
}
