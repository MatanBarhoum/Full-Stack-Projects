import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
@Component({
  selector: 'app-show-dep',
  templateUrl: './show-dep.component.html',
  styleUrls: ['./show-dep.component.css']
})
export class ShowDepComponent implements OnInit {

  constructor(private service:SharedService) { }

  DepartmentList:any=[];

  ModalTitle: string;
  ActivateAddEditDepComp:boolean;
  dep:any;
  DepartmentId: string;
  DepartmentName: string;
  ngOnInit(): void {
    this.refreshDepList();
    this.DepartmentId=this.dep.departmentId;
    this.DepartmentName=this.dep.DepartmentName;
  }

  addClick() {
    this.dep={
      DepartmentId:0,
      DepartmentName:""
    }
    this.ModalTitle="Add Department";
    this.ActivateAddEditDepComp=true;
  }

  closeClick() {
    this.ActivateAddEditDepComp=false;
    this.refreshDepList();
  }
  
  editClick(item:any) {
    this.dep = item;
    //this.dep.DepartmentId = item.DepartmentId;
    this.ModalTitle="Edit Department";
    this.ActivateAddEditDepComp=true;
  }

  deleteClick(item:any) {
    this.dep = item;
    var val = this.dep.DepartmentId; 
    this.service.deleteDepartment(val).subscribe(result=>{
      alert(result.toString());
      this.refreshDepList();
    })
    
  }

  refreshDepList(){ //Async opertaion
    this.service.getDepList().subscribe(data=>{
      this.DepartmentList=data;
    });
  }
}
