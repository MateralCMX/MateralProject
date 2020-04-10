export class FormHelper {
    public value: any = {};
    private formElement: HTMLFormElement;
    private inputElements: any = {};
    private errorInputClass = 'm-form-item-error';
    private errorMessageClass = 'm-form-item-error-message';
    public constructor(private formID: string) {
        this.formElement = document.getElementById(this.formID) as HTMLFormElement;
        if (!this.formElement) throw `未找到ID为${this.formID}的Html元素`;
        const inputElements = this.formElement.getElementsByTagName('input');
        for (let i = 0; i < inputElements.length; i++) {
            const element = inputElements[i];
            this.initInput(element);
        }
    }
    /**
     * 设置Disabled
     * @param state 状态
     */
    public setDisabled(state: boolean) {
        if (state) {
            for (const key in this.inputElements) {
                if (this.inputElements.hasOwnProperty(key)) {
                    const element = this.inputElements[key] as HTMLInputElement;
                    element.setAttribute('disabled', 'disabled');
                }
            }
        } else {
            for (const key in this.inputElements) {
                if (this.inputElements.hasOwnProperty(key)) {
                    const element = this.inputElements[key] as HTMLInputElement;
                    element.removeAttribute('disabled');
                }
            }
        }
    }
    /**
     * 验证表单
     */
    public checkValidity(): boolean {
        for (const key in this.inputElements) {
            if (this.inputElements.hasOwnProperty(key)) {
                const element = this.inputElements[key] as HTMLInputElement;
                this.removeInputErrorStyle(element);
            }
        }
        const result = this.formElement.checkValidity();
        return result;
    }
    /**
     * 移除Input错误样式
     * @param element InputElement对象
     */
    private removeInputErrorStyle(element: HTMLInputElement) {
        if (element.classList.contains(this.errorInputClass)) {
            element.classList.remove(this.errorInputClass);
        }
        if (element.parentNode) {
            const message = (element.parentNode as HTMLElement).getAttribute('m-message');
            const errorMessageSpan = element.parentNode.lastChild as HTMLSpanElement;
            if (message && errorMessageSpan && errorMessageSpan.tagName === 'SPAN' && errorMessageSpan.classList.contains(this.errorMessageClass)) {
                errorMessageSpan.remove();
            }
        }
    }
    /**
     * 初始化Input
     * @param element InputElment对象
     */
    private initInput(element: HTMLInputElement) {
        const name = element.getAttribute('m-name');
        this.inputElements[name] = element;
        this.value[name] = element.value;
        element.addEventListener('change', () => {
            this.value[name] = element.value;
            this.removeInputErrorStyle(element);
            element.checkValidity();
        });
        element.addEventListener('invalid', () => this.onInputInvalid(element));
    }
    /**
     * Input验证失败事件
     * @param element inputElement对象
     */
    private onInputInvalid(element: HTMLInputElement) {
        if (!element.classList.contains(this.errorInputClass)) {
            element.classList.add(this.errorInputClass);
            if (element.parentNode) {
                const message = (element.parentNode as HTMLElement).getAttribute('m-message');
                if (message) {
                    const errorMessageSpan = document.createElement('span');
                    errorMessageSpan.classList.add(this.errorMessageClass);
                    errorMessageSpan.innerText = message;
                    element.parentNode.appendChild(errorMessageSpan);
                }
            }
        }
    }
}