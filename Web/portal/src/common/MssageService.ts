export class MssageService {
    public static info(message: string) {
        const messageBox = new MessageBox(message, MessageBoxType.info);
        messageBox.show();
    }
    public static warning(message: string) {
        const messageBox = new MessageBox(message, MessageBoxType.warning);
        messageBox.show();
    }
    public static error(message: string) {
        const messageBox = new MessageBox(message, MessageBoxType.error);
        messageBox.show();
    }
    public static success(message: string) {
        const messageBox = new MessageBox(message, MessageBoxType.success);
        messageBox.show();
    }
}
export class MessageBox {
    private static messageGroupElement: HTMLDivElement;
    private messageBoxElement: HTMLDivElement;
    constructor(private message: string, private type: MessageBoxType) {
        if (!MessageBox.messageGroupElement) {
            MessageBox.messageGroupElement = document.createElement('div');
            MessageBox.messageGroupElement.classList.add('m-messagebox-group');
            window.document.body.appendChild(MessageBox.messageGroupElement);
        }
        this.messageBoxElement = document.createElement('div');
        this.messageBoxElement.classList.add('m-messagebox');
        switch (this.type) {
            case MessageBoxType.success:
                this.messageBoxElement.classList.add('m-messagebox-success');
                break;
            case MessageBoxType.warning:
                this.messageBoxElement.classList.add('m-messagebox-warning');
                break;
            case MessageBoxType.error:
                this.messageBoxElement.classList.add('m-messagebox-error');
                break;
            default:
                break;
        }
        const messageDiv = document.createElement('div');
        messageDiv.innerText = this.message;
        this.messageBoxElement.appendChild(messageDiv);
    }
    public show() {
        MessageBox.messageGroupElement.appendChild(this.messageBoxElement);
        setTimeout(() => {
            this.messageBoxElement.remove();
        }, 3000);
    }
}
export enum MessageBoxType {
    info = 0,
    success = 1,
    warning = 2,
    error = 3
}