import console from 'console';
export const i = 5;

console?.log('test');

export default class Test {
  static inject = [console];
  constructor(private ss: console) {
  }
}
