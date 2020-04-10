import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-delete-modal',
  templateUrl: './delete-modal.component.html',
  styleUrls: ['./delete-modal.component.less']
})
export class DeleteModalComponent {
  public isVisible = false;
  private id: string;
  public isLoading = false;
  public constructor() { }
  @Output()
  public deleteHandler = new EventEmitter<string>();
  /**
   * 打开
   * @param id 唯一标识
   */
  public open(id: string) {
    this.isVisible = true;
    this.id = id;
  }
  /**
   * 关闭
   */
  public close() {
    this.isVisible = false;
    this.isLoading = false;
  }
  /**
   * 处理确定
   */
  public handleOK() {
    this.isLoading = true;
    if (this.deleteHandler) {
      this.deleteHandler.emit(this.id);
    }
  }
}
