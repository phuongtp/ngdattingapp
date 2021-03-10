import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.scss']
})
export class MemberListComponent implements OnInit {
  members$: Observable<Member[]> = of([]); // = new Observable<Member[]>();

  constructor(private memberService: MembersService) {
    // this.members = of(this.arrayMembers);
  }

  ngOnInit(): void {
    this.members$ = this.memberService.getMembers();
  }

  // loadMembers() {
  //   this.memberService.getMembers().subscribe(members => {
  //     this.members = members;
  //   });
  // }

}
